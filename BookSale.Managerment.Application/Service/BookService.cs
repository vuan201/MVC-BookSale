﻿using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using BookSale.Managerment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSale.Managerment.Domain.Extension;
using System.Net;

namespace BookSale.Managerment.Application.Service
{
    public class BookService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _cloudinaryService;
        private readonly IFileService _fileService;

        public BookService(IMapper mapper, IUnitOfWork unitOfWork, IStorageServiceFactory storageServiceFactory, IFileService fileService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryService = storageServiceFactory.GetStorageService(StorageType.Cloudinary);
            _fileService = fileService;
        }

        public async Task<ResponseModel<BookDetailDTO>> GetBook(int id)
        {
            if(id <= 0) return new ResponseModel<BookDetailDTO>(false, ResponseMessage.InvalidValue);

            var book = await _unitOfWork.BookRepository.GetBookById(id);

            if (book is null) return new ResponseModel<BookDetailDTO>(false, ResponseMessage.DoesNotExist);

            var bookDTO = _mapper.Map<BookDetailDTO>(book);

            return new ResponseModel<BookDetailDTO>(true, ResponseMessage.GetDataSuccess, bookDTO);
        }
        public async Task<ResponseModel<IEnumerable<BookDTO>>> GetBookList(RequestFilterModel filter)
        {
            var query = _unitOfWork.BookRepository.AsQueryable();

            if (!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(i => i.Name.Contains(filter.search));
            }

            var total = query.Count();

            var genres = await query.Include(i => i.Genres)
                                    .Include(i => i.BookTags)
                                    .ThenInclude(i => i.Tags)
                                    .Include(i => i.BookImages)
                                    .ThenInclude(i => i.Images)
                                    .Skip(filter.Offset)
                                    .Take(filter.Limit)
                                    .ToListAsync();

            var listGenre = _mapper.Map<IEnumerable<BookDTO>>(genres);

            return new ResponseModel<IEnumerable<BookDTO>>(true, ResponseMessage.GetDataSuccess, total, listGenre);
        }
        public async Task<ResponseModel<BookDetailDTO>> Create(BookDetailDTO bookDetail)
        {
            if (bookDetail is null) 
                return new ResponseModel<BookDetailDTO>(false, ResponseMessage.InvalidValue);

            try
            {
                // Tạo một model mới và map dữ liệu qua
                var newBook = new Books
                {
                    Code = UniqueCodeGenerator.GenerateUniqueGuid()
                };
                
                _mapper.Map(bookDetail, newBook);
                
                // Lưu sách vào database
                await _unitOfWork.BookRepository.CreateAsync(newBook);
                await _unitOfWork.SaveChangesAsync();
                
                // Xử lý upload ảnh song song nếu có
                await ProcessBookImages(bookDetail, newBook);
                
                // Xử lý thêm tags nếu có
                await ProcessBookTags(bookDetail, newBook);
                
                // Map dữ liệu từ entity trở lại DTO để trả về
                _mapper.Map(newBook, bookDetail);
                
                return new ResponseModel<BookDetailDTO>(true, ResponseMessage.CreateSuccess, bookDetail);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return new ResponseModel<BookDetailDTO>(false, $"Lỗi khi tạo sách: {ex.Message}");
            }
        }
        
        // Phương thức riêng để xử lý hình ảnh của sách
        private async Task ProcessBookImages(BookDetailDTO bookDetail, Books newBook)
        {
            if (bookDetail.ImageFiles?.Any() != true)
                return;
                
            var uploadResult = await _cloudinaryService.UploadListImage(bookDetail.ImageFiles.ToList());
            
            if (!uploadResult.Status || uploadResult.Data == null)
                return;
                
            // Upload ảnh lên storage
            var listFileDTO = uploadResult.Data
                .Select(i => new FIleDTO(
                    StorageType.Cloudinary, 
                    newBook.Code + i.PublicId, 
                    i.PublicId ?? string.Empty))
                .ToList();
                
            // Lưu thông tin về db
            var saveFileResult = await _fileService.SaveFiles(listFileDTO, StorageType.Cloudinary);
            
            if (!saveFileResult.Status || saveFileResult.Data == null)
                return;
                
            // Lưu vào BookImage
            foreach (var fileDto in saveFileResult.Data)
            {
                newBook.BookImages.Add(new BookImages { BookId = newBook.Id, ImageId = fileDto.Id });
            }
                
            await _unitOfWork.SaveChangesAsync();
        }
        
        // Phương thức riêng để xử lý tags của sách
        private async Task ProcessBookTags(BookDetailDTO bookDetail, Books newBook)
        {
            if (bookDetail.BookTags?.Any() != true)
                return;
                
            foreach (var tag in bookDetail.BookTags)
            {
                newBook.BookTags.Add(new BookTags { BookId = newBook.Id, TagId = tag.Id });
            }
                
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

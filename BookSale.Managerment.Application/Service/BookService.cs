﻿﻿﻿﻿﻿using AutoMapper;
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
using BookSale.Managerment.Application.FakeData;
using Microsoft.AspNetCore.Identity;

namespace BookSale.Managerment.Application.Service
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _cloudinaryService;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookService(IMapper mapper, IUnitOfWork unitOfWork, IStorageServiceFactory storageServiceFactory, IFileService fileService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryService = storageServiceFactory.GetStorageService(StorageType.Cloudinary);
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task<ResponseModel<BookDetailDTO>> GetBook(int id)
        {
            if (id <= 0) return new ResponseModel<BookDetailDTO>(false, ResponseMessage.InvalidValue);

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

            if(listGenre.Count() == 0) listGenre = FakeData.FakeData.GenerateFakeBooks(59);

            return new ResponseModel<IEnumerable<BookDTO>>(true, ResponseMessage.GetDataSuccess, total, listGenre);
        }
        public async Task<ResponseModel<BookDetailDTO>> Create(BookDetailDTO bookDetail)
        {
            if (bookDetail is null)
                return new ResponseModel<BookDetailDTO>(false, ResponseMessage.InvalidValue);

            // Validate AuthorId và GenreId
            if (string.IsNullOrEmpty(bookDetail.AuthorId))
                return new ResponseModel<BookDetailDTO>(false, "AuthorId không được để trống");

            if (bookDetail.GenreId <= 0)
                return new ResponseModel<BookDetailDTO>(false, "GenreId không hợp lệ");

            try
            {
                // Tạo một model mới và map dữ liệu qua
                var newBook = new Books();

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

        public async Task<ResponseModel<BookDetailDTO>> Update(int id, BookDetailDTO bookDetail)
        {
            if (id <= 0 || bookDetail is null)
                return new ResponseModel<BookDetailDTO>(false, ResponseMessage.InvalidValue);

            // Validate AuthorId và GenreId
            if (string.IsNullOrEmpty(bookDetail.AuthorId))
                return new ResponseModel<BookDetailDTO>(false, "AuthorId không được để trống");

            if (bookDetail.GenreId <= 0)
                return new ResponseModel<BookDetailDTO>(false, "GenreId không hợp lệ");

            try
            {
                // Tìm sách cần cập nhật
                var existingBook = await _unitOfWork.BookRepository.GetBookById(id);
                
                if (existingBook is null)
                    return new ResponseModel<BookDetailDTO>(false, ResponseMessage.DoesNotExist);

                // Map dữ liệu mới vào entity hiện tại (giữ nguyên Code và Id)
                var originalCode = existingBook.Code;
                var originalId = existingBook.Id;
                
                _mapper.Map(bookDetail, existingBook);
                
                // Đảm bảo không thay đổi Code và Id
                existingBook.Code = originalCode;
                existingBook.Id = originalId;

                // Cập nhật sách trong database
                _unitOfWork.BookRepository.Update(existingBook);
                await _unitOfWork.SaveChangesAsync();

                // Xử lý cập nhật ảnh nếu có
                await UpdateBookImages(bookDetail, existingBook);

                // Xử lý cập nhật tags nếu có
                await UpdateBookTags(bookDetail, existingBook);

                // Map dữ liệu từ entity trở lại DTO để trả về
                var updatedBookDTO = _mapper.Map<BookDetailDTO>(existingBook);

                return new ResponseModel<BookDetailDTO>(true, ResponseMessage.UpdateSuccess, updatedBookDTO);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return new ResponseModel<BookDetailDTO>(false, $"Lỗi khi cập nhật sách: {ex.Message}");
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
            if (bookDetail.BookTagIds?.Any() != true)
                return;

            if(newBook.BookTags is null) newBook.BookTags = new List<BookTags>();

            foreach (var id in bookDetail.BookTagIds)
            {
                newBook.BookTags.Add(new BookTags { BookId = newBook.Id, TagId = id });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        // Phương thức riêng để cập nhật hình ảnh của sách
        private async Task UpdateBookImages(BookDetailDTO bookDetail, Books existingBook)
        {
            // Nếu không có ảnh mới thì không làm gì
            if (bookDetail.ImageFiles?.Any() != true)
                return;

            // Xóa các ảnh cũ
            var oldBookImages = existingBook.BookImages.ToList();
            foreach (var oldImage in oldBookImages)
            {
                existingBook.BookImages.Remove(oldImage);
            }

            // Upload ảnh mới lên storage
            var uploadResult = await _cloudinaryService.UploadListImage(bookDetail.ImageFiles.ToList());

            if (!uploadResult.Status || uploadResult.Data == null)
                return;

            // Tạo danh sách file DTO
            var listFileDTO = uploadResult.Data
                .Select(i => new FIleDTO(
                    StorageType.Cloudinary,
                    existingBook.Code + i.PublicId,
                    i.PublicId ?? string.Empty))
                .ToList();

            // Lưu thông tin file vào database
            var saveFileResult = await _fileService.SaveFiles(listFileDTO, StorageType.Cloudinary);

            if (!saveFileResult.Status || saveFileResult.Data == null)
                return;

            if(existingBook.BookTags is null) existingBook.BookTags = new List<BookTags>();

            // Thêm ảnh mới vào BookImage
            foreach (var fileDto in saveFileResult.Data)
            {
                existingBook.BookImages.Add(new BookImages { BookId = existingBook.Id, ImageId = fileDto.Id });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        // Phương thức riêng để cập nhật tags của sách
        private async Task UpdateBookTags(BookDetailDTO bookDetail, Books existingBook)
        {
            // Nếu không có tags mới thì không làm gì
            if (bookDetail.BookTagIds?.Any() != true)
                return;

            // Xóa các tags cũ
            var oldBookTags = existingBook.BookTags.ToList();
            foreach (var oldTag in oldBookTags)
            {
                existingBook.BookTags.Remove(oldTag);
            }

            // Thêm tags mới
            foreach (var id in bookDetail.BookTagIds)
            {
                existingBook.BookTags.Add(new BookTags { BookId = existingBook.Id, TagId = id });
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Abstract;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Application.Service
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<FIleDTO>> SaveFile(FIleDTO fileDto)
        {
            if (fileDto is null || fileDto.Key is null || fileDto.Name is null)
            {
                return new ResponseModel<FIleDTO>(false, "Dữ liệu không hợp lệ");
            }

            var cloudStorage = _unitOfWork.CloudStorageRepository.GetCloudStorageByType(fileDto.StorageType);
            if (cloudStorage is null)
            {
                return new ResponseModel<FIleDTO>(false, "Loại lưu trữ không tồn tại");
            }

            var newFile = _mapper.Map<Files>(fileDto);
            newFile.CloudStorageId = cloudStorage.Id;

            if (fileDto.Id != 0)
            {
                // Lấy file cũ có trong database
                var oldFile = await _unitOfWork.FileRepository.GetAsync(i => i.Id == fileDto.Id);
                if (oldFile is null)
                {
                    return new ResponseModel<FIleDTO>(false, "File không tồn tại");
                }

                // Cập nhật file
                _mapper.Map(newFile, oldFile);
            }
            else
            {
                // Tạo File
                await _unitOfWork.FileRepository.CreateAsync(newFile);
            }

            // Lưu lại thay đổi
            await _unitOfWork.SaveChangesAsync();

            // Map lại để có Id sau khi lưu 
            _mapper.Map(newFile, fileDto);

            return new ResponseModel<FIleDTO>(true, "Lưu thành công", fileDto);
        }
        public async Task<ResponseModel<List<FIleDTO>>> SaveFiles(List<FIleDTO> fileDtos, StorageType storageType)
        {
            if (fileDtos == null || fileDtos.Count == 0)
            {
                return new ResponseModel<List<FIleDTO>>(false, "Danh sách file không hợp lệ hoặc rỗng");
            }

            var cloudStorage = _unitOfWork.CloudStorageRepository.GetCloudStorageByType(storageType);
            if (cloudStorage is null)
            {
                return new ResponseModel<List<FIleDTO>>(false, "Kiểu kho lưu trữ không tồn tại") ; 
            }

            var resultList = new List<FIleDTO>();

            foreach (var fileDto in fileDtos)
            {
                if (fileDto is null || fileDto.Key is null || fileDto.Name is null)
                {
                    continue; // Bỏ qua file không hợp lệ
                }

                var newFile = _mapper.Map<Files>(fileDto);
                newFile.CloudStorageId = cloudStorage.Id;

                if (fileDto.Id != 0)
                {
                    var oldFile = await _unitOfWork.FileRepository.GetAsync(i => i.Id == fileDto.Id);
                    if (oldFile == null)
                    {
                        continue; // Bỏ qua nếu không tìm thấy file
                    }

                    _mapper.Map(newFile, oldFile);
                }
                else
                {
                    await _unitOfWork.FileRepository.CreateAsync(newFile);
                }

                // Sau khi xử lý xong từng file, map lại vào DTO để lấy Id
                _mapper.Map(newFile, fileDto);
                resultList.Add(fileDto);
            }

            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<List<FIleDTO>>(true, "Lưu thành công", resultList);
        }

    }
}

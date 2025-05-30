using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Entity;
using BookSale.Managerment.Domain.Enums;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IFileService
    {
        Task<ResponseModel<FIleDTO>> SaveFile(FIleDTO fileDto);
        Task<ResponseModel<List<FIleDTO>>> SaveFiles(List<FIleDTO> fileDtos, StorageType storageType);
    }
}
using BookSale.Managerment.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace BookSale.Managerment.Application.Abstracts
{
    public interface IStorageService
    {
        Task<ResponseModel<CloudinaryResponse>> UploadImage(IFormFile file, string fileName);
        Task<ResponseModel<List<CloudinaryResponse>>> UploadListImage(List<IFormFile> files);
        string GetUrlImageByPublicId(string publicId);
        List<FIleDTO> SetUrlImageByPublicId(List<FIleDTO> files);
        Task<ResponseModel<string>> RemoveImage(string publicId);
    }
}
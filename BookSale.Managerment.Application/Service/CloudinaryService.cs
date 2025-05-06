using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.constants;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.AspNetCore.Http;

namespace BookSale.Managerment.Application.Service
{
    public class CloudinaryService : IStorageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        public CloudinaryService(IMapper mapper)
        {
            DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { Setup.EnvPath }));

            this._cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"))
            {
                Api = { Secure = true } // Bắt buộc dùng HTTPS
            };
            this._mapper = mapper;
        }

        public async Task<ResponseModel> UploadImage(IFormFile file, string fileKey)
        {
            if (file == null || file.Length <= 0)
            {
                return new ResponseModel(false, "File không hợp lệ!");
            }

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileKey, stream),
                PublicId = fileKey,
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                Transformation = new Transformation().FetchFormat("png") // Chuyển đổi định dạng sang PNG
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return new ResponseModel(false, uploadResult.Error.Message);

            return new ResponseModel<CloudinaryResponse>(_mapper.Map<CloudinaryResponse>(uploadResult));
        }
        public string GetUrlImageByPublicId(string publicId) => _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
    }
}
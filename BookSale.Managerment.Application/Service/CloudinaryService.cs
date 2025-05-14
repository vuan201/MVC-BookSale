using AutoMapper;
using BookSale.Managerment.Application.Abstracts;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.constants;
using BookSale.Managerment.Domain.Enums;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.AspNetCore.Http;
namespace BookSale.Managerment.Application.Service
{
    /*
    * ✅ Các thuộc tính chính của ImageUploadParams
        Thuộc tính	    Kiểu dữ liệu	                        Mô tả

        File	        FileDescription	                        Đường dẫn file hoặc stream để upload. Đây là thuộc tính bắt buộc.
        PublicId	    string	                                Đặt tên tệp tùy chỉnh thay vì để Cloudinary tự sinh.
        Folder	        string	                                Đường dẫn thư mục nơi file sẽ được lưu.
        Overwrite	    bool	                                Ghi đè file đã tồn tại nếu đặt là true.
        UseFilename	    bool	                                Sử dụng tên file gốc làm public_id.
        UniqueFilename	bool	                                Tự động thêm hậu tố duy nhất để tránh trùng tên.
        Tags	        string || List<string>	                Gán thẻ cho ảnh.
        Transformation	Transformation	                        Chứa các thao tác xử lý ảnh (resize, crop, rotate, ...).
        Context	        string || Dictionary<string, string>	Metadata tuỳ chỉnh cho ảnh (ví dụ: `alt=Ảnh đại diện
        Type	        string	                                Loại upload (upload, private, authenticated,...). Mặc định là "upload".
        NotificationUrl	string	                                URL để nhận callback sau khi upload xong.
        EagerTransforms	List<Transformation>	                Danh sách các biến thể xử lý ảnh (eager transformations).
        ResourceType	string	                                "image", "video", hoặc "raw". Mặc định là "image".
    */


    public class CloudinaryService : IStorageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly ICloudStorageService _cloudStorageService;
        public CloudinaryService(IMapper mapper, ICloudStorageService cloudStorageService)
        {
            _cloudStorageService = cloudStorageService;
            var storage = _cloudStorageService.GetStorage(StorageType.Cloudinary);

            DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { Setup.EnvPath }));

            this._cloudinary = storage != null
                              ? new Cloudinary(new Account(storage.Account, storage.Key, storage.Secret))
                              {
                                  Api = { Secure = true } // Bắt buộc dùng HTTPS
                              }
                              : new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"))
                              {
                                  Api = { Secure = true } // Bắt buộc dùng HTTPS
                              };

            this._mapper = mapper;
        }

        public async Task<ResponseModel<CloudinaryResponse>> UploadImage(IFormFile file, string fileName)
        {
            if (file == null || file.Length <= 0)
            {
                return new ResponseModel<CloudinaryResponse>(false, "File không hợp lệ!");
            }

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, stream),
                UseFilename = false,
                UniqueFilename = false,
                Overwrite = true,
                Transformation = new Transformation().FetchFormat("png") // Chuyển đổi định dạng sang PNG
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return new ResponseModel<CloudinaryResponse>(false, uploadResult.Error.Message);

            return new ResponseModel<CloudinaryResponse>(true, "Upload thành công!", _mapper.Map<CloudinaryResponse>(uploadResult));
        }
        public string GetUrlImageByPublicId(string publicId) => _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
    }
}
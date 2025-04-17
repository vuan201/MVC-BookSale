// using BookSale.Managerment.Application.Abstracts;
// using BookSale.Managerment.Application.DTOs;
// using BookSale.Managerment.Domain.Extension;
// using AutoMapper;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using Amazon.S3;
// using Amazon.S3.Model;
// using Amazon.S3.Transfer;

// namespace BookSale.Managerment.Application.Service
// {
//     public class AwsS3StorageService : IStorageService
//     {
//         private readonly IAmazonS3 _s3Client;
//         private readonly IMapper _mapper;
//         private readonly string _bucketName;
//         private readonly string _region;

//         public AwsS3StorageService(IMapper mapper)
//         {
//             DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { Setup.EnvPath }));
            
//             string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
//             string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
//             _bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME");
//             _region = Environment.GetEnvironmentVariable("AWS_REGION") ?? "ap-southeast-1";
            
//             var config = new AmazonS3Config
//             {
//                 RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_region)
//             };
            
//             _s3Client = new AmazonS3Client(accessKey, secretKey, config);
//             _mapper = mapper;
//         }

//         public async Task<ResponseModel> UploadImage(IFormFile file, string fileKey)
//         {
//             if (file == null || file.Length <= 0)
//             {
//                 return new ResponseModel(false, "File không hợp lệ!");
//             }

//             try
//             {
//                 // Tạo đường dẫn lưu trữ trên S3
//                 string s3Key = $"images/{fileKey}";
                
//                 // Upload file lên S3
//                 using var transferUtility = new TransferUtility(_s3Client);
//                 await using var stream = file.OpenReadStream();
                
//                 var uploadRequest = new TransferUtilityUploadRequest
//                 {
//                     InputStream = stream,
//                     BucketName = _bucketName,
//                     Key = s3Key,
//                     CannedACL = S3CannedACL.PublicRead
//                 };
                
//                 await transferUtility.UploadAsync(uploadRequest);
                
//                 // Tạo URL
//                 string s3Url = $"https://{_bucketName}.s3.{_region}.amazonaws.com/{s3Key}";
                
//                 // Tạo response
//                 var response = new CloudinaryResponse
//                 {
//                     PublicId = fileKey,
//                     Url = s3Url,
//                     SecureUrl = s3Url
//                 };
                
//                 return new ResponseModel<CloudinaryResponse>(response);
//             }
//             catch (Exception ex)
//             {
//                 return new ResponseModel(false, $"Lỗi khi upload lên AWS S3: {ex.Message}");
//             }
//         }

//         public string GetUrlImageByPublicId(string publicId)
//         {
//             string s3Key = $"images/{publicId}";
//             return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{s3Key}";
//         }
//     }
// }
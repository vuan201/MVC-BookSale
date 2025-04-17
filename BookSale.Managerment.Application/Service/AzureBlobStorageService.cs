// using BookSale.Managerment.Application.Abstracts;
// using BookSale.Managerment.Application.DTOs;
// using BookSale.Managerment.Domain.Extension;
// using AutoMapper;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using Azure.Storage.Blobs;
// using Azure.Storage.Blobs.Models;

// namespace BookSale.Managerment.Application.Service
// {
//     public class AzureBlobStorageService : IStorageService
//     {
//         private readonly BlobServiceClient _blobServiceClient;
//         private readonly IMapper _mapper;
//         private readonly string _containerName;

//         public AzureBlobStorageService(IMapper mapper)
//         {
//             DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { Setup.EnvPath }));
            
//             string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
//             _containerName = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER_NAME") ?? "images";
            
//             _blobServiceClient = new BlobServiceClient(connectionString);
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
//                 // Lấy container client
//                 var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                
//                 // Tạo container nếu chưa tồn tại
//                 await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
                
//                 // Lấy blob client
//                 var blobClient = containerClient.GetBlobClient(fileKey);
                
//                 // Upload file
//                 await using var stream = file.OpenReadStream();
//                 await blobClient.UploadAsync(stream, true);
                
//                 // Lấy URL
//                 string blobUrl = blobClient.Uri.ToString();
                
//                 // Tạo response
//                 var response = new CloudinaryResponse
//                 {
//                     PublicId = fileKey,
//                     Url = blobUrl,
//                     SecureUrl = blobUrl
//                 };
                
//                 return new ResponseModel<CloudinaryResponse>(response);
//             }
//             catch (Exception ex)
//             {
//                 return new ResponseModel(false, $"Lỗi khi upload lên Azure Blob Storage: {ex.Message}");
//             }
//         }

//         public string GetUrlImageByPublicId(string publicId)
//         {
//             var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
//             var blobClient = containerClient.GetBlobClient(publicId);
//             return blobClient.Uri.ToString();
//         }
//     }
// }
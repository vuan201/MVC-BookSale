// using BookSale.Managerment.Application.Abstracts;
// using BookSale.Managerment.Application.DTOs;
// using BookSale.Managerment.Domain.Extension;
// using AutoMapper;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.IO;
// using System.Threading.Tasks;
// using Firebase.Storage;

// namespace BookSale.Managerment.Application.Service
// {
//     public class FirebaseStorageService : IStorageService
//     {
//         private readonly FirebaseStorage _firebaseStorage;
//         private readonly IMapper _mapper;
//         private readonly string _storageBucket;

//         public FirebaseStorageService(IMapper mapper)
//         {
//             DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { Setup.EnvPath }));
            
//             _storageBucket = Environment.GetEnvironmentVariable("FIREBASE_STORAGE_BUCKET");
//             string apiKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY");
            
//             _firebaseStorage = new FirebaseStorage(_storageBucket, 
//                 new FirebaseStorageOptions
//                 {
//                     AuthTokenAsyncFactory = () => Task.FromResult(apiKey),
//                     ThrowOnCancel = true
//                 });
            
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
//                 await using var stream = file.OpenReadStream();
                
//                 // Tạo đường dẫn lưu trữ trên Firebase
//                 string storagePath = $"images/{fileKey}";
                
//                 // Upload file lên Firebase Storage
//                 var uploadTask = await _firebaseStorage
//                     .Child(storagePath)
//                     .PutAsync(stream);
                
//                 // Lấy URL download
//                 string downloadUrl = uploadTask;
                
//                 // Tạo response
//                 var response = new CloudinaryResponse
//                 {
//                     PublicId = fileKey,
//                     Url = downloadUrl,
//                     SecureUrl = downloadUrl
//                 };
                
//                 return new ResponseModel<CloudinaryResponse>(response);
//             }
//             catch (Exception ex)
//             {
//                 return new ResponseModel(false, $"Lỗi khi upload lên Firebase: {ex.Message}");
//             }
//         }

//         public string GetUrlImageByPublicId(string publicId)
//         {
//             // Tạo đường dẫn lưu trữ trên Firebase
//             string storagePath = $"images/{publicId}";
            
//             // Trả về URL của file
//             return _firebaseStorage
//                 .Child(storagePath)
//                 .GetDownloadUrlAsync()
//                 .GetAwaiter()
//                 .GetResult();
//         }
//     }
// }
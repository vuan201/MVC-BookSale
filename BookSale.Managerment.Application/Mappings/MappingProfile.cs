using AutoMapper;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Entity;
using CloudinaryDotNet.Actions;

namespace BookSale.Managerment.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap()

                // Phớt lờ Id khi map từ UserDto -> ApplicationUser
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                // Chỉ map UserName nếu đích (ApplicationUser.UserName) chưa có giá trị
                .ForMember(
                    dest => dest.UserName, 
                    opt => opt.Condition((src, dest, srcMember, destMember) => destMember == null)
                );

            CreateMap<Files, Files>().ForMember(dest => dest.Id, opt => opt.Ignore());

            // Cloudinary response mapping
            CreateMap<ImageUploadResult, CloudinaryResponse>();

            // File mappings
            CreateMap<Files, FIleDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 


            // Genre mappings
            CreateMap<Genres, GenreDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); ;

            // BookImage mappings
            CreateMap<BookImages, BookImageDTO>().ReverseMap();
        }
    }
}
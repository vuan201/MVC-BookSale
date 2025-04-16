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
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Kh�ng map Id
                .ForMember(dest => dest.UserName, opt => opt.Condition((src, dest, srcMember, destMember) => destMember == null));
            
            // Cloudinary response mapping
            CreateMap<ImageUploadResult, CloudinaryResponse>();
            
            // Genre mappings
            CreateMap<Genres, GenreDTO>().ReverseMap();

            // BookImage mappings
            CreateMap<BookImages, BookImageDTO>().ReverseMap();

            // Add more mappings as needed for other entities and DTOs
        }
    }
}
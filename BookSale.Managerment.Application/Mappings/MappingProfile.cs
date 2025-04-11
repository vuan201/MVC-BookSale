using AutoMapper;
using BookSale.Managerment.Application.DTOs;
using BookSale.Managerment.Domain.Entity;

namespace BookSale.Managerment.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserRequestModel>();
            CreateMap<UserRequestModel, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Không map Id
            // CreateMap<List<ApplicationUser>, List<UserDTO>>().ReverseMap();

            // Genre mappings
            CreateMap<Genres, GenreDTO>().ReverseMap();

            // BookImage mappings
            CreateMap<BookImages, BookImageDTO>().ReverseMap();

            // Add more mappings as needed for other entities and DTOs
        }
    }
}
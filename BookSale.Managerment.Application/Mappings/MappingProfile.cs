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
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Tag Mapping
            CreateMap<Tags, TagDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // BookImage mappings
            CreateMap<BookImages, BookImageDTO>().ReverseMap();

            // Book mappings
            CreateMap<Books, BookDTO>()
                // tạo map trường GenreName và AuthorName từ nguồn Genre và Author
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))

                // Tạo trường BookTags từ nguồn BookTags
                // Sử dụng Select() để lấy danh sách các tên tag của mỗi cuốn sách
                .ForMember(dest => dest.BookTags, opt => opt.MapFrom(src => src.BookTags.Select(i => i.Tags)));

            CreateMap<Books, BookDetailDTO>()
                // tạo map trường GenreName và AuthorName từ nguồn Genre và Author
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))

                // Tạo trường BookTags từ nguồn BookTags
                // Sử dụng Select() để lấy danh sách các tên tag của mỗi cuốn sách
                .ForMember(dest => dest.BookTagIds, opt => opt.MapFrom(src => src.BookTags.Select(i => i.Tags.Id)))

                // Tạo trường BookImages từ BookImages => Images
                .ForMember(dest => dest.BookImages, opt => opt.MapFrom(src => src.BookImages.Select(i => i.Images)))
                .ReverseMap()
                .ForMember(dest => dest.BookTags, opt => opt.Ignore())
                .ForMember(dest => dest.BookImages, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
            ;
        }
    }
}
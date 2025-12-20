using AutoMapper;
using LibraryApi.Entities;
using LibraryApi.DTO;

namespace LibraryApi.Mapping
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile() 
        {
            // Category
            CreateMap<Category, CategoryDto>();

            // Book → BookDto (custom mapping)
            CreateMap<CreateBookDto, Book>();
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName,
                            //opt => opt.MapFrom(src => src.Category!.Name));
                            opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "Unknown"));

            // CreateBookDto → Book
            CreateMap<CreateBookDto, Book>();

            // UpdateBookDto → Book
            CreateMap<UpdateBookDto, Book>();

            // Borrow → BorrowDto
            CreateMap<Borrow, BorrowDto>()
                .ForMember(dest => dest.BookTitle,
                           //opt => opt.MapFrom(src => src.Book!.Title));
                           opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : "Unknown"));

            // CreateBorrowDto → Borrow
            CreateMap<CreateBorrowDto, Borrow>();
        }
    }
}

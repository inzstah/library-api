using AutoMapper;
using Library.Domain.Data;

namespace Library.DTO
{
    public class MyProfile : Profile
    {
        public MyProfile() {
            CreateMap<User, UserDto>();
            CreateMap<Book, BookDto>();
            CreateMap<Author, AuthorDto>();
        }
    }
}

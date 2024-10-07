using AutoMapper;
using Library.Domain.Data;
namespace Library.API.DTO
{
    public class MyProfile : Profile
    {
        public MyProfile() {
            CreateMap<User, UserDTO>();
            CreateMap<Book, BookDTO>();
            CreateMap<Author, AuthorDTO>();
        }
    }
}

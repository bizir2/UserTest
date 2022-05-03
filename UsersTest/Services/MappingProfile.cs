using AutoMapper;
using UsersTest.Entites;
using UserTestLibrary.Models.User;

namespace UsersTest.Services
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<UserDto, UserDocument>();
            CreateMap<UserDocument, UserDto>();
        }
    }
}

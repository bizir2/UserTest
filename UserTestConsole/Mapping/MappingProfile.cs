using AutoMapper;
using UserTestLibrary.Models.CreateUser;
using UserTestLibrary.Models.RemoveUser;
using UserTestLibrary.Models.User;

namespace UserTestConsole.Mapping
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<string[], CreateUser>()
                .ForPath(x => x.User.Id, opt => opt.MapFrom(x => x.Length > 0 ? x[0] : null))
                .ForPath(x => x.User.Name, opt => opt.MapFrom(x => x.Length > 0 ? x[1] : null))
                .ForPath(x => x.User.Status, opt => opt.MapFrom(x => x.Length > 0 ? x[2] : null));
            CreateMap<string[], RemoveUser>()
                .ForPath(x => x.RemoveUserId.Id, opt => opt.MapFrom(x => x.Length > 0 ? x[0] : null));
            CreateMap<string[], UserDto>()
                .ForPath(x => x.Id, opt => opt.MapFrom(x => x.Length > 0 ? x[0] : null))
                .ForPath(x => x.Status, opt => opt.MapFrom(x => x.Length > 0 ? x[1] : null));
        }
    }
}

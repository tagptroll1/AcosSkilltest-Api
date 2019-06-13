using AutoMapper;
using Acos.ProgrammingTask.Models;

namespace Acos.ProgrammingTask.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
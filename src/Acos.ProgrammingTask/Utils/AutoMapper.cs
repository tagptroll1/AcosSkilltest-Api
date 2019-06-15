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
            CreateMap<Whiteboard, WhiteboardDtoIn>();
            CreateMap<Whiteboard, WhiteboardDtoOut>();
            CreateMap<WhiteboardDtoIn, Whiteboard>();
            CreateMap<WhiteboardDtoIn, WhiteboardDtoOut>();
            CreateMap<Postit, PostitDtoIn>();
            CreateMap<PostitDtoIn, Postit>();
            CreateMap<TodoDto, Todo>();
            CreateMap<Todo, TodoDto>();
        }
    }
}
using AutoMapper;
using CoreWebApiBoilerPlate.DataLayer.Entities;
using CoreWebApiBoilerPlate.Models;

namespace CoreWebApiBoilerPlate.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<NewUserRequestModel, User>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(m => m.Password, opt => opt.MapFrom(src => EasyEncryption.MD5.ComputeMD5Hash(src.Password)));


            CreateMap<UserRequestModel, User>();


            CreateMap<RoleRequestModel, Role>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<User, UserResponseModel>();



            CreateMap<Role, RoleResponseModel>();

            CreateMap<TodoRequestModel, Todo>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => Constants.CurrentUserId))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<Todo, TodoResponseModel>();
            CreateMap<Comment, CommentResponseModel>();

            CreateMap<CommentRequestModel, Comment>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => Constants.CurrentUserId));
        }


    }
}

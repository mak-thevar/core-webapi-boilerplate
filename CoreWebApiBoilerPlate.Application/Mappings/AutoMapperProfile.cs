using AutoMapper;
using CoreWebApiBoilerPlate.Application.DTO;
using CoreWebApiBoilerPlate.Application.DTO.Request;
using CoreWebApiBoilerPlate.Application.DTO.Response;
using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;

namespace CoreWebApiBoilerPlate.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<NewUserRequestDTO, User>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
                //.ForMember(m => m.Password, opt => opt.MapFrom(src => EasyEncryption.MD5.ComputeMD5Hash(src.Password)));


            CreateMap<NewUserRequestDTO, User>();


            CreateMap<RoleRequestModel, Role>()
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<User, UserResponseDTO>();



            CreateMap<Role, RoleResponseModel>();

            CreateMap<TodoRequestModel, Todo>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => Constants.CurrentUserId))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<Todo, TodoResponseModel>();

            CreateMap<Comment, CommentResponseModel>();

            CreateMap<CommentRequestDTO, Comment>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.CreatedById, opt => opt.MapFrom(src => Constants.CurrentUserId));
        }


    }
}

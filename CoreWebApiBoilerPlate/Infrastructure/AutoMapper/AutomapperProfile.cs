using AutoMapper;
using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Models;
using System;
using NETCore.Encrypt.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Infrastructure.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PostRequestModel, Post>()
                .ForMember(pr => pr.CreatedOn, dest => dest.MapFrom(m => DateTime.Now))
                .ForMember(pr => pr.CreatedBy, dest => dest.MapFrom(m => Constants.CurrentUserId));

            CreateMap<UserRequestModel, User>()
                    .ForMember(pr=>pr.IsActive, dest=>dest.MapFrom(m=>true))
                    .ForMember(pr=>pr.Password, dest=>dest.MapFrom(m=>m.ConfirmPassword.SHA1()));
        }
    }
}

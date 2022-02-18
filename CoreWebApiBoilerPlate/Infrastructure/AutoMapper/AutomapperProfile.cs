using AutoMapper;
using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Models;
using System;
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
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;
using Work.Api.Models;

namespace Work.Api.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(
                dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name));
            CreateMap<CompanyAddDto, Company>();
        }
    }
}

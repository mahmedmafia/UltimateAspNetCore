﻿using AutoMapper;
using Entities.Models;
using Shared.DTO;

namespace CompanyEmployees
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForCtorParam("FullAddress",
                opt => opt.MapFrom(entity => string.Join(" ", entity.Address, entity.Country)));
        }
    }
}

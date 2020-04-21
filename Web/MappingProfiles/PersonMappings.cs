using AutoMapper;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.MappingProfiles
{
    public class PersonMappings : Profile
    {
        public PersonMappings()
        {
            CreateMap<PhoneModel, Phone>();
            CreateMap<RelationModel, Relation>();
            CreateMap<PersonModel,Person>().ForMember(dest => dest.Phones,
                opt => opt.MapFrom(src => src.Phones));
        }
    }
}

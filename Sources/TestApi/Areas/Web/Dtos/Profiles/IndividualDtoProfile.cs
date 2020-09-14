using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos.Profiles
{
    public class IndividualDtoProfile : Profile
    {
        public IndividualDtoProfile()
        {
            CreateMap<Individual, IndividualDto>()
                .ForMember(d => d.Birthdate, c => c.MapFrom(f => f.Birthdate))
                .ForMember(d => d.FirstName, c => c.MapFrom(f => f.FirstName))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.LastName, c => c.MapFrom(f => f.LastName));
        }
    }
}

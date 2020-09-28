using AutoMapper;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModels;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.Adapters.Implementation.Profiles
{
    public class IndividualDataModelProfile : Profile
    {
        public IndividualDataModelProfile()
        {
            CreateMap<Individual, IndividualDataModel>()
                .ForMember(d => d.Birthdate, c => c.MapFrom(f => f.Birthdate))
                .ForMember(d => d.FirstName, c => c.MapFrom(f => f.FirstName))
                .ForMember(d => d.Id, c => c.MapFrom(f => f.Id))
                .ForMember(d => d.LastName, c => c.MapFrom(f => f.LastName));
        }
    }
}
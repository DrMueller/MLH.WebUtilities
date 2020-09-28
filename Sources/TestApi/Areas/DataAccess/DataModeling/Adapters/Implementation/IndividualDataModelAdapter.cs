using AutoMapper;
using Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModels;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.Adapters.Implementation
{
    public class IndividualDataModelAdapter : IDataModelAdapter<Individual, IndividualDataModel>
    {
        private readonly IIndividualFactory _individualFactory;
        private readonly IMapper _mapper;

        public IndividualDataModelAdapter(IMapper mapper, IIndividualFactory individualFactory)
        {
            _mapper = mapper;
            _individualFactory = individualFactory;
        }

        public Individual Adapt(IndividualDataModel entity)
        {
            return _individualFactory.Create(
                entity.FirstName,
                entity.LastName,
                entity.Birthdate,
                entity.Id);
        }

        public IndividualDataModel Adapt(Individual aggregateRoot)
        {
            return _mapper.Map<IndividualDataModel>(aggregateRoot);
        }
    }
}
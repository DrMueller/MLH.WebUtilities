using AutoMapper;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Services.Implementation;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Factories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.Adapters
{
    public class IndividualDataModelAdapter : DataModelAdapterBase<IndividualDataModel, Individual, long>
    {
        private readonly IIndividualFactory _individualFactory;

        public IndividualDataModelAdapter(IMapper mapper, IIndividualFactory individualFactory) : base(mapper)
        {
            _individualFactory = individualFactory;
        }

        public override Individual Adapt(IndividualDataModel dataModel)
        {
            return _individualFactory.Create(
                dataModel.FirstName,
                dataModel.LastName,
                dataModel.Birthdate,
                dataModel.Id);
        }
    }
}
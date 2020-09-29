using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndividualsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _uowFactory;

        public IndividualsController(IUnitOfWorkFactory uowFactory, IMapper mapper)
        {
            _uowFactory = uowFactory;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<IndividualDto>> CreateIndividualAsync(IndividualDto dto)
        {
            var entity = _mapper.Map<Individual>(dto);

            using var uow = _uowFactory.Create();
            var individualRepo = uow.GetRepository<IIndividualRepository>();
            await individualRepo.UpsertAsync(entity);
            await uow.SaveAsync();

            var returnedEntity = await individualRepo.LoadByIdAsync(entity.Id);
            var resultDto = _mapper.Map<IndividualDto>(returnedEntity);
            return Ok(resultDto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<IndividualDto>>> GetAllAsync()
        {
            using var uow = _uowFactory.Create();
            var individualRepo = uow.GetRepository<IIndividualRepository>();
            var allIndividuals = await individualRepo.LoadAllAsync();

            var dtos = allIndividuals.Select(ind => _mapper.Map<IndividualDto>(ind)).ToList();

            return Ok(dtos);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndividualsController : ControllerBase
    {
        private readonly IIndividualRepository _individualRepo;
        private readonly IMapper _mapper;

        public IndividualsController(IIndividualRepository individualRepo, IMapper mapper)
        {
            _individualRepo = individualRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<IndividualDto>> CreateIndividualAsync(IndividualDto dto)
        {
            var entity = _mapper.Map<Individual>(dto);
            var returnedEntity = await _individualRepo.SaveAsync(entity);

            var resultDto = _mapper.Map<IndividualDto>(returnedEntity);

            return Ok(resultDto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<IndividualDto>>> GetAllAsync()
        {
            var allIndividuals = await _individualRepo.LoadAllIndividualsAsync();
            var dtos = allIndividuals.Select(ind => _mapper.Map<IndividualDto>(ind)).ToList();

            return Ok(dtos);
        }
    }
}
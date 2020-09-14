using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndividualsController : ControllerBase
    {
        private readonly IIndividualRepository _individualRepo;
        private readonly IMapper _mapper;

        public IndividualsController(IIndividualRepository individualRepo, IMapper mapper)
        {
            _individualRepo = individualRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<IndividualDto>>> GetAllAsync()
        {
            var allIndividuals = await _individualRepo.LoadAllAsync();

            var dtos = allIndividuals.Select(ind => _mapper.Map<IndividualDto>(ind)).ToList();

            return Ok(dtos);
        }

    }
}

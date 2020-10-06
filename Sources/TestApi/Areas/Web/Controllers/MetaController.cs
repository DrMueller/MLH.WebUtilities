using Microsoft.AspNetCore.Mvc;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.UnitOfWorks.Repositories;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly IIndividualRepository _individualRepo;
        private readonly IOrganisationRepository _organisationRepo;

        public MetaController(IOrganisationRepository organisationRepo, IIndividualRepository individualRepo)
        {
            _organisationRepo = organisationRepo;
            _individualRepo = individualRepo;
        }

        [HttpGet("notmocked")]
        public ActionResult<string> GetIndividualRepoTypeName()
        {
            return Ok(_individualRepo.GetType().Name);
        }

        [HttpGet("mocked")]
        public ActionResult<string> GetOrganisationRepoTypeName()
        {
            return Ok(_organisationRepo.GetType().Name);
        }
    }
}
using JetBrains.Annotations;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities
{
    [PublicAPI]
    public class Address : EntityBase
    {
        public long OrganisationId { get; set; }
        public string StreetName { get; set; }
    }
}
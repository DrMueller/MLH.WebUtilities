using JetBrains.Annotations;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities
{
    [PublicAPI]
    public class Address : EntityBase
    {
        public string StreetName { get; set; }
    }
}
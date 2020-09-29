using System.Collections.Generic;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities
{
    public class Organisation : EntityBase
    {
        public ICollection<Address> Addresses { get; set; }
        public string Name { get; set; }
    }
}
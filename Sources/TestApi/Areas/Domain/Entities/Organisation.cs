using System.Collections.Generic;
using JetBrains.Annotations;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities
{
    [PublicAPI]
    public class Organisation : EntityBase
    {
        public ICollection<Address> Addresses { get; set; }
        public string Name { get; set; }
    }
}
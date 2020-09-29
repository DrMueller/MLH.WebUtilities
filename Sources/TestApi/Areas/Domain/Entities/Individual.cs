using System;
using JetBrains.Annotations;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities
{
    [PublicAPI]
    public class Individual : EntityBase
    {
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
using System;
using JetBrains.Annotations;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities
{
    [PublicAPI]
    public class Individual
    {
        public long Id { get; set; }
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
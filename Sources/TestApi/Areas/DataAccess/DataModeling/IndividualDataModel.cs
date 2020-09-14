using System;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling
{
    public class IndividualDataModel : AggregateRootDataModel<long>
    {
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
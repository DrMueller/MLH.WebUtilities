using System;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.DataModels
{
    public class IndividualDataModel
    {
        public long Id { get; set; }
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
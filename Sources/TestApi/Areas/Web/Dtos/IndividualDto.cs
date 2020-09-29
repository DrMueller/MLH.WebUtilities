using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Web.Dtos
{
    public class IndividualDto : IValidatableObject
    {
        public DateTime Birthdate { get; set; }

        public string FirstName { get; set; }

        public long Id { get; set; }

        [Required]
        public string LastName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Birthdate == DateTime.MinValue)
            {
                yield return new ValidationResult("Birthdate has to be set", new[] { nameof(Birthdate) });
            }
        }
    }
}
using System;
using Mmu.Mlh.DomainExtensions.Areas.DomainModeling;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models
{
    public class Individual : AggregateRoot<long>
    {
        private string _firstName;

        public DateTime Birthdate { get; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Guard.ObjectNotNull(() => FirstName);
            }
        }

        public string LastName { get; }

        public Individual(
            string firstName,
            string lastName,
            DateTime birthdate,
            long id) : base(id)
        {
            Guard.ObjectNotNull(() => lastName);

            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
        }
    }
}
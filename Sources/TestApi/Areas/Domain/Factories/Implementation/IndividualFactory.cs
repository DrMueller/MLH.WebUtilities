using System;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Factories.Implementation
{
    public class IndividualFactory : IIndividualFactory
    {
        public Individual Create(
            string firstName,
            string lastName,
            DateTime birthdate,
            long? id = null)
        {
            return new Individual(
                firstName,
                lastName,
                birthdate,
                id ?? 0);
        }
    }
}
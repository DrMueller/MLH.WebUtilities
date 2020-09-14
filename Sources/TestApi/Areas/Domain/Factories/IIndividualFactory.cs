using System;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Factories
{
    public interface IIndividualFactory
    {
        Individual Create(
            string firstName,
            string lastName,
            DateTime birthdate,
            long? id = null);
    }
}
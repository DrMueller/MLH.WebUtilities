﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Models;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Repositories
{
    public interface IIndividualRepository
    {
        public Task<IReadOnlyCollection<Individual>> LoadAllIndividualsAsync();
    }
}
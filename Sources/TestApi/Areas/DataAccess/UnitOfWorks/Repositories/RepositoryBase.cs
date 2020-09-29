using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.WebUtilities.TestApi.Areas.Domain.Entities;

namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.UnitOfWorks.Repositories
{
    // We can't currently cast to generic types, so we ease a ungeneric one for easeness
    public abstract class RepositoryBase
    {
        public abstract void Initialize(DbContext dbContext);
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase
        where TEntity : EntityBase
    {
        protected DbSet<TEntity> DbSet { get; private set; }

        public override void Initialize(DbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> LoadAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task UpsertAsync(TEntity entity)
        {
            if (entity.Id.Equals(default))
            {
                await DbSet.AddAsync(entity);
            }
            else
            {
                DbSet.Update(entity);
            }
        }
    }
}
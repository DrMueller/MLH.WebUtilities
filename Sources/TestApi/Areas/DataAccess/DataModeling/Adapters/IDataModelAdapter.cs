namespace Mmu.Mlh.WebUtilities.TestApi.Areas.DataAccess.DataModeling.Adapters
{
    public interface IDataModelAdapter<TAggregateRoot, TEntity>
    {
        TAggregateRoot Adapt(TEntity entity);

        TEntity Adapt(TAggregateRoot aggregateRoot);
    }
}
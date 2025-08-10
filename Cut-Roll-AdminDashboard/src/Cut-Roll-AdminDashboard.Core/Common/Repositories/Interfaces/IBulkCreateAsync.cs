namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface IBulkCreateAsync<TEntity, TResult>
{
    public Task<TResult> BulkCreateAsync(IEnumerable<TEntity> listToCreate);
}

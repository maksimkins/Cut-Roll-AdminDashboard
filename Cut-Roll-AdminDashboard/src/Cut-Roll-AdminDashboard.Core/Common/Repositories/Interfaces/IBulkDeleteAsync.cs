namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface IBulkDeleteAsync<TEntity, TResult>
{
    public Task<TResult> BulkDeleteAsync(IEnumerable<TEntity> listToDelete);
}

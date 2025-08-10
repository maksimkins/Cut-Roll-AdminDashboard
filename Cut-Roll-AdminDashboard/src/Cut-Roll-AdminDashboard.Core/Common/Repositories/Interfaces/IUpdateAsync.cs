namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface IUpdateAsync<TEntity, TReturn>
{
    Task<TReturn> UpdateAsync(TEntity entity);
}

namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface ICreateAsync<TEntity, TReturn>
{
    Task<TReturn> CreateAsync(TEntity entity);
}

namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface IGetByIdAsync<TId, TReturnEntity>
{
    Task<TReturnEntity> GetByIdAsync(TId id);
}

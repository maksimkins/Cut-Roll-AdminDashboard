namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface IDeleteByIdAsync<TId, TReturn> 
{
    Task<TReturn> DeleteByIdAsync(TId id);
}

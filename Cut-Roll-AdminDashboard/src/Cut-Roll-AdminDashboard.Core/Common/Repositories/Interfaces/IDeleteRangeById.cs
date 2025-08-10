namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface IDeleteRangeById<TId, TReturn>
{
    Task<TReturn> DeleteRangeById(TId id);
}

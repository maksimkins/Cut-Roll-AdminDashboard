namespace Cut_Roll_AdminDashboard.Core.Common.Repositories.Interfaces;

public interface ISearchAsync<TRequest, TResponse>
{
    Task<TResponse> SearchAsync(TRequest request);
}


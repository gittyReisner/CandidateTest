using Requests.Domain.Entities;

public interface IRequestRepository
{
    IQueryable<Request> GetQuery();

    Task<List<Request>> ToListAsync(
        IQueryable<Request> query,
        CancellationToken cancellationToken = default);
}
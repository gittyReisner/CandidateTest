using Microsoft.EntityFrameworkCore;
using Requests.Application.Requests;
using Requests.Domain.Entities;
using Requests.Infrastructure.Persistence;

namespace Requests.Infrastructure.Repositories;

public sealed class RequestRepository : IRequestRepository
{
    private readonly RequestsDbContext _db;

    public RequestRepository(RequestsDbContext db)
    {
        _db = db;
    }

    public IQueryable<Request> GetQuery()
    {
        return _db.Requests.AsQueryable();
    }

    public Task<List<Request>> ToListAsync(
        IQueryable<Request> query,
        CancellationToken cancellationToken = default)
    {
        return EntityFrameworkQueryableExtensions.ToListAsync(
            query,
            cancellationToken);
    }
}
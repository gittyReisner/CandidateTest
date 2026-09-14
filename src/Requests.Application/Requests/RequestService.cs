namespace Requests.Application.Requests;

public sealed class RequestService : IRequestService
{
    private readonly IRequestRepository _repository;

    public RequestService(IRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<RequestDto>> GetRequestsAsync(
        int currentUserId,
        bool isAdministrator,
        RequestFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = _repository.GetQuery();

        // Authorization - regular users can only see
        // requests they own or are assigned to.
        if (!isAdministrator)
        {
            query = query.Where(x =>
                x.OwnerId == currentUserId ||
                x.AssignedToUserId == currentUserId);
        }

        if (!string.IsNullOrWhiteSpace(filter?.RequestNumber))
        {
            query = query.Where(x =>
                x.RequestNumber.Contains(filter.RequestNumber));
        }

        if (filter?.Statuses is { Count: > 0 })
        {
            query = query.Where(x =>
                filter.Statuses.Contains(x.Status));
        }

        if (filter?.FromDate.HasValue == true)
        {
            query = query.Where(x =>
                x.CreatedAt >= filter.FromDate.Value);
        }

        if (filter?.ToDate.HasValue == true)
        {
            query = query.Where(x =>
                x.CreatedAt <= filter.ToDate.Value);
        }

        if (filter?.RequestType.HasValue == true)
        {
            query = query.Where(x =>
                x.RequestType == filter.RequestType.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter?.SortBy))
        {
            query = filter.SortBy.ToLowerInvariant() switch
            {
                "requestnumber" => filter.SortDescending
                    ? query.OrderByDescending(x => x.RequestNumber)
                    : query.OrderBy(x => x.RequestNumber),

                "createdat" => filter.SortDescending
                    ? query.OrderByDescending(x => x.CreatedAt)
                    : query.OrderBy(x => x.CreatedAt),

                "status" => filter.SortDescending
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),

                "requesttype" => filter.SortDescending
                    ? query.OrderByDescending(x => x.RequestType)
                    : query.OrderBy(x => x.RequestType),

                _ => throw new ArgumentException(
                    $"Invalid sort field: {filter.SortBy}")
            };
        }

        var skip = (filter?.Page - 1 ?? 0) * (filter?.PageSize ?? 50);

        var pageSize = filter?.PageSize ?? 50;

        query = query
            .Skip(skip)
            .Take(pageSize);

        var requests = await _repository.ToListAsync(
            query,
            cancellationToken);
        return requests.Select(x => new RequestDto(
            x.Id,
            x.RequestNumber,
            x.CustomerId,
            x.OwnerId,
            x.AssignedToUserId,
            x.Status,
            x.RequestType,
            x.CreatedAt)).ToList();
    }
}
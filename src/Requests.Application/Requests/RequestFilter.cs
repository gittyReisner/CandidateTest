using Requests.Domain.Entities;

public sealed record RequestFilter
{
    public string? RequestNumber { get; init; }

    public IReadOnlyList<RequestStatus>? Statuses { get; init; }

    public DateTime? FromDate { get; init; }

    public DateTime? ToDate { get; init; }

    public RequestType? RequestType { get; init; }

    public string? SortBy { get; init; }

    public bool SortDescending { get; init; }

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 50;
}
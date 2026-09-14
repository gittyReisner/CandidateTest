using Microsoft.AspNetCore.Mvc;
using Requests.Application.Requests;

namespace Requests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestsController : ControllerBase
{
    private readonly IRequestService _service;

    public RequestsController(IRequestService service)
    {
        _service = service;
    }

    // For the exercise, the current user is supplied through headers:
    // X-User-Id: integer
    // X-Is-Admin: true|false
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RequestDto>>> Get(
        [FromQuery] RequestFilter? filter,
        CancellationToken cancellationToken)
    {
        var userId = ParseUserId(Request.Headers["X-User-Id"].FirstOrDefault());

        var isAdmin = string.Equals(
            Request.Headers["X-Is-Admin"].FirstOrDefault(),
            "true",
            StringComparison.OrdinalIgnoreCase);

        if (filter?.FromDate.HasValue == true &&
            filter?.ToDate.HasValue == true &&
            filter.FromDate > filter.ToDate)
        {
            return BadRequest("FromDate cannot be later than ToDate.");
        }

        if (filter is not null &&
            (filter.Page < 1 || filter.PageSize < 1 || filter.PageSize > 100))
        {
            return BadRequest("Page must be at least 1 and PageSize must be between 1 and 100.");
        }

        try
        {
            var result = await _service.GetRequestsAsync(
                userId,
                isAdmin,
                filter,
                cancellationToken);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private static int ParseUserId(string? value)
        => int.TryParse(value, out var userId) ? userId : 1;
}

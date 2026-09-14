using Requests.Application.Requests;
using Requests.Domain.Entities;
using Xunit;

namespace Requests.Tests
{
    public class RequestServiceTests
    {
        [Fact]
        public async Task Administrator_CanSeeAllRequests()
        {
            var repository = new FakeRequestRepository(new List<Request>
            {
                Create(1, ownerId: 1, assignedTo: 2),
                Create(2, ownerId: 3, assignedTo: 4)
            });

            var service = new RequestService(repository);

            var result = await service.GetRequestsAsync(1, true);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task RegularUser_CanSeeOwnedOrAssignedRequests()
        {
            var repository = new FakeRequestRepository(new List<Request>
            {
                Create(1, ownerId: 1, assignedTo: 5),
                Create(2, ownerId: 3, assignedTo: 1),
                Create(3, ownerId: 3, assignedTo: 5)
            });

            var service = new RequestService(repository);

            var result = await service.GetRequestsAsync(1, false);

            Assert.Equal(2, result.Count);
            Assert.DoesNotContain(result, x => x.Id == 3);
        }

        [Fact]
        public async Task CanFilterByPartialRequestNumber()
        {
            var repository = new FakeRequestRepository(new List<Request>
            {
                Create(1, ownerId: 1, assignedTo: 5),
                Create(2, ownerId: 1, assignedTo: 5),
                Create(3, ownerId: 1, assignedTo: 5)
            });

            var service = new RequestService(repository);

            var filter = new RequestFilter { RequestNumber = "REQ-001" };

            var result = await service.GetRequestsAsync(1, false, filter);

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        private static Request Create(
            int id,
            int ownerId,
            int assignedTo,
            RequestStatus status = RequestStatus.New,
            RequestType requestType = RequestType.General,
            DateTime? createdAt = null)
            => new Request
            {
                Id = id,
                RequestNumber = $"REQ-{id:000}",
                CustomerId = id,
                OwnerId = ownerId,
                AssignedToUserId = assignedTo,
                Status = status,
                RequestType = requestType,
                CreatedAt = createdAt ?? DateTime.UtcNow
            };

        private sealed class FakeRequestRepository : IRequestRepository
        {
            private readonly List<Request> _requests;

            public FakeRequestRepository(List<Request> requests) => _requests = requests;

            public IQueryable<Request> GetQuery() => _requests.AsQueryable();

            public Task<List<Request>> ToListAsync(IQueryable<Request> query, CancellationToken cancellationToken = default)
                => Task.FromResult(query.ToList());
        }
    }
}
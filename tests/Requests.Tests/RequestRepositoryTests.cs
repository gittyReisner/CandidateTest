using Microsoft.EntityFrameworkCore;
using Requests.Domain.Entities;
using Requests.Infrastructure.Persistence;
using Requests.Infrastructure.Repositories;
using Xunit;

namespace Requests.Tests
{
    public class RequestRepositoryTests
    {
        [Fact]
        public async Task ToListAsync_ReturnsAllRequests()
        {
            var options = new DbContextOptionsBuilder<RequestsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var db = new RequestsDbContext(options);
            db.Requests.AddRange(
                new Request { Id = 1, RequestNumber = "REQ-001", CustomerId = 1, OwnerId = 1, Status = RequestStatus.New, RequestType = RequestType.General, CreatedAt = DateTime.UtcNow },
                new Request { Id = 2, RequestNumber = "REQ-002", CustomerId = 2, OwnerId = 2, Status = RequestStatus.InProgress, RequestType = RequestType.Legal, CreatedAt = DateTime.UtcNow },
                new Request { Id = 3, RequestNumber = "REQ-003", CustomerId = 3, OwnerId = 3, Status = RequestStatus.Completed, RequestType = RequestType.Payment, CreatedAt = DateTime.UtcNow }
            );
            db.SaveChanges();

            var repo = new RequestRepository(db);
            var list = await repo.ToListAsync(repo.GetQuery());

            Assert.Equal(3, list.Count);
            Assert.Contains(list, r => r.RequestNumber == "REQ-001");
            Assert.Contains(list, r => r.RequestNumber == "REQ-002");
            Assert.Contains(list, r => r.RequestNumber == "REQ-003");
        }
    }
}
using MongoDB.Driver;
using Moq;
using Webhooks.DataAccess.Interfaces;
using Webhooks.DataAccess.Models.Entities;
using Webhooks.DataAccess.Repositories;
using Xunit;

namespace Webhooks.DataAccess.UnitTests.Repositories
{
    public class GenericRepositoryTests
    {
        private readonly Mock<IWebhooksDbContext> _webhooksDbContextMock;

        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IGenericRepository<Invoice> _repository;

        public GenericRepositoryTests()
        {
            const string databaseName = "DatabaseName";

            _mongoClient = new MongoClient();
            _mongoDatabase = _mongoClient.GetDatabase(databaseName);

            _webhooksDbContextMock = new Mock<IWebhooksDbContext>();
            _repository = new GenericRepository<Invoice>(_webhooksDbContextMock.Object);
        }

        [Fact]
        public async Task Invoice_ShouldAdd_Success()
        {
            // Arrange
            const string collectionName = "CollectionName";

            var invoice = new Invoice
            {
                Created = DateTime.UtcNow,
                Currency = string.Empty,
                Date = DateTime.UtcNow,
                Description = string.Empty,
                Discount = 0,
                DueDate = DateTime.UtcNow,
                HasApproved = true,
                Id = Guid.NewGuid(),
                InvoiceFrom = string.Empty,
                InvoiceTo = string.Empty,
                IsActive = true,
                Number = 0,
                Price = 0,
                Quantity = 0,
                Tax = 0,
                Total = 0,
                Updated = DateTime.UtcNow
            };

            _webhooksDbContextMock.Setup(x => x.GetCollection<Invoice>(It.IsAny<string>())).Returns(_mongoDatabase.GetCollection<Invoice>(collectionName));

            // Action
            await _repository.AddAsync(invoice);

            // Assert
            _webhooksDbContextMock.Verify(x => x.GetCollection<Invoice>(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShoudUpdate_Success()
        {
            // Arrange
            const string collectionName = "CollectionName";

            var invoice = new Invoice
            {
                Created = DateTime.UtcNow,
                Currency = string.Empty,
                Date = DateTime.UtcNow,
                Description = string.Empty,
                Discount = 0,
                DueDate = DateTime.UtcNow,
                HasApproved = true,
                Id = Guid.NewGuid(),
                InvoiceFrom = string.Empty,
                InvoiceTo = string.Empty,
                IsActive = true,
                Number = 0,
                Price = 0,
                Quantity = 0,
                Tax = 0,
                Total = 0,
                Updated = DateTime.UtcNow
            };

            _webhooksDbContextMock.Setup(x => x.GetCollection<Invoice>(It.IsAny<string>())).Returns(_mongoDatabase.GetCollection<Invoice>(collectionName));

            // Action
            await _repository.UpdateAsync(invoice);

            // Assert
            _webhooksDbContextMock.Verify(x => x.GetCollection<Invoice>(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldDelete_Success()
        {
            // Arrange
            const string collectionName = "CollectionName";

            var invoiceId = Guid.NewGuid();

            _webhooksDbContextMock.Setup(x => x.GetCollection<Invoice>(It.IsAny<string>())).Returns(_mongoDatabase.GetCollection<Invoice>(collectionName));

            // Action
            await _repository.DeleteAsync(x => x.IsActive && x.Id == invoiceId);

            // Assert
            _webhooksDbContextMock.Verify(x => x.GetCollection<Invoice>(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldGet_Success()
        {
            // Arrange
            const string collectionName = "CollectionName";

            var invoiceId = Guid.NewGuid();

            _webhooksDbContextMock.Setup(x => x.GetCollection<Invoice>(It.IsAny<string>())).Returns(_mongoDatabase.GetCollection<Invoice>(collectionName));

            // Action
            await _repository.GetAsync(x => x.IsActive && x.Id == invoiceId);

            // Assert
            _webhooksDbContextMock.Verify(x => x.GetCollection<Invoice>(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldGetAll_Success()
        {
            // Arrange
            const string collectionName = "CollectionName";

            var invoiceId = Guid.NewGuid();

            _webhooksDbContextMock.Setup(x => x.GetCollection<Invoice>(It.IsAny<string>())).Returns(_mongoDatabase.GetCollection<Invoice>(collectionName));

            // Action
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);

            _webhooksDbContextMock.Verify(x => x.GetCollection<Invoice>(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldSaveChanges_Success()
        {
            // Arrange
            _webhooksDbContextMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Action
            await _repository.SaveChangesAsync();

            // Assert
            _webhooksDbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }
    }
}

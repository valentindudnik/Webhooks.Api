using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Webhooks.DataAccess.Interfaces;
using Webhooks.DataAccess.Models.Entities;
using Webhooks.Infrastructure.Profiles;
using Webhooks.Models.Events;
using Webhooks.Services.Interfaces;
using Webhooks.Services.Interfaces.Producers;
using Xunit;

namespace Webhooks.Services.UnitTests.Services
{
    public class InvoiceServiceTests
    {
        private readonly Mock<IGenericRepository<Invoice>> _repositoryMock;
        private readonly Mock<IInvoiceProducer> _invoiceProducerMock;

        private readonly IInvoiceService _service;

        public InvoiceServiceTests()
        {
            _repositoryMock = new Mock<IGenericRepository<Invoice>>();
            _invoiceProducerMock = new Mock<IInvoiceProducer>();

            var mapper = new Mapper(new MapperConfiguration(configure => configure.AddProfile<InvoiceProfile>()));
            
            var loggerMock = new Mock<ILogger<InvoiceService>>();

            _service = new InvoiceService(_repositoryMock.Object, _invoiceProducerMock.Object, mapper, loggerMock.Object);
        }

        [Fact]
        public async Task Invoice_ShouldAdd_Success()
        {
            // Arrange
            var parameters = new Models.Parameters.InvoiceParameters {
                Currency = string.Empty,
                Date = DateTime.UtcNow,
                Description = string.Empty,
                Discount = 0,
                DueDate = DateTime.UtcNow,
                HasApproved = true,
                InvoiceFrom = string.Empty,
                InvoiceTo = string.Empty,
                Price = 0,
                Quantity = 0,
                Tax = 0,
                Total = 0
            };

            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Invoice>())).Returns(Task.FromResult(true));

            // Action
            var result = await _service.AddAsync(parameters);

            // Assert
            Assert.NotNull(result);

            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Invoice>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldUpdate_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            var parameters = new Models.Parameters.InvoiceParameters
            {
                Currency = string.Empty,
                Date = DateTime.UtcNow,
                Description = string.Empty,
                Discount = 0,
                DueDate = DateTime.UtcNow,
                HasApproved = true,
                InvoiceFrom = string.Empty,
                InvoiceTo = string.Empty,
                Price = 0,
                Quantity = 0,
                Tax = 0,
                Total = 0
            };

            var invoice = new Invoice
            {
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
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _repositoryMock.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>())).Returns(Task.FromResult<Invoice?>(invoice));

            _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Invoice>())).Returns(Task.FromResult(true));

            // Action
            await _service.UpdateAsync(invoiceId, parameters);

            // Assert
            _repositoryMock.Verify(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>()), Times.Once());

            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Invoice>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldDelete_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            var invoice = new Invoice
            {
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
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _repositoryMock.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>())).Returns(Task.FromResult<Invoice?>(invoice));

            _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Invoice>())).Returns(Task.FromResult(true));

            // Action
            await _service.DeleteAsync(invoiceId);

            // Assert
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Invoice>()), Times.Once());

            _repositoryMock.Verify(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldGet_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            var invoice = new Invoice
            {
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
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _repositoryMock.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>())).Returns(Task.FromResult<Invoice?>(invoice));

            // Action
            var result = await _service.GetAsync(invoiceId);

            // Assert
            Assert.NotNull(result);

            _repositoryMock.Verify(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldGetAll_Success()
        {
            // Arrange
            var invoice = new Invoice
            {
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
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync((IEnumerable<Invoice>)new List<Invoice> {
                invoice,
                invoice
            });

            // Action
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);

            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldApprove_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            var invoice = new Invoice
            {
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
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _repositoryMock.Setup(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>())).Returns(Task.FromResult<Invoice?>(invoice));

            _invoiceProducerMock.Setup(x => x.Approved(It.IsAny<ApproveInvoiceEvent>()));

            // Action
            await _service.ApproveAsync(invoiceId);

            // Assert
            _repositoryMock.Verify(x => x.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Invoice, bool>>>()), Times.Once());

            _invoiceProducerMock.Verify(x => x.Approved(It.IsAny<ApproveInvoiceEvent>()));
        }
    }
}

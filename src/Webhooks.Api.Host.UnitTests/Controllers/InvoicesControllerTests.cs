using Moq;
using Webhooks.Api.Host.Controllers;
using Webhooks.Models.Dtos;
using Webhooks.Services.Interfaces;
using Xunit;

namespace Webhooks.Api.Host.UnitTests.Controllers
{
    public class InvoicesControllerTests
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock;

        private readonly InvoicesController _controller;

        public InvoicesControllerTests()
        {
            _invoiceServiceMock = new Mock<IInvoiceService>();

            _controller = new InvoicesController(_invoiceServiceMock.Object);
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

            var entityResult = new Models.Results.EntityResult
            {
                Id = Guid.NewGuid()
            };

            _invoiceServiceMock.Setup(x => x.AddAsync(It.IsAny<Models.Parameters.InvoiceParameters>())).Returns(Task.FromResult(entityResult));

            // Action
            var result = await _controller.AddAsync(parameters);

            // Assert
            Assert.NotNull(result);

            _invoiceServiceMock.Verify(x => x.AddAsync(It.IsAny<Models.Parameters.InvoiceParameters>()), Times.Once());
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

            _invoiceServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Models.Parameters.InvoiceParameters>())).Returns(Task.FromResult(true));

            // Action
            var result = await _controller.UpdateAsync(invoiceId, parameters);

            // Assert
            Assert.NotNull(result);

            _invoiceServiceMock.Verify(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Models.Parameters.InvoiceParameters>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldDelete_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            _invoiceServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            // Action
            var result = await _controller.DeleteAsync(invoiceId);

            // Assert
            Assert.NotNull(result);

            _invoiceServiceMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldGet_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            _invoiceServiceMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Models.Dtos.InvoiceDto { 
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
            }));

            // Action
            var result = await _controller.GetAsync(invoiceId);

            // Assert
            Assert.NotNull(result);

            _invoiceServiceMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldGetAll_Success()
        {
            // Arrange
            _invoiceServiceMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult((IEnumerable<InvoiceDto>)new List<InvoiceDto> { 
                new InvoiceDto {
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
                }
            }));

            // Action
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.NotNull(result);

            _invoiceServiceMock.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task Invoice_ShouldApprove_Success()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();

            _invoiceServiceMock.Setup(x => x.ApproveAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            // Action
            var result = await _controller.ApproveAsync(invoiceId);

            // Assert
            Assert.NotNull(result);

            _invoiceServiceMock.Verify(x => x.ApproveAsync(It.IsAny<Guid>()), Times.Once());
        }
    }
}

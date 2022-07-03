using Microsoft.Extensions.Logging;
using Moq;
using Webhooks.RabbitMQ.Client.Interfaces;
using Webhooks.Services.Interfaces.Producers;
using Webhooks.Services.Producers;
using Xunit;

namespace Webhooks.Services.UnitTests.Producers
{
    public class InvoiceProducerTests
    {
        private readonly Mock<IRabbitMQClient> _rabbitMQClientMock;

        private readonly IInvoiceProducer _invoiceProducer;

        public InvoiceProducerTests()
        {
            _rabbitMQClientMock = new Mock<IRabbitMQClient>();

            var logger = new Mock<ILogger<InvoiceProducer>>();

            _invoiceProducer = new InvoiceProducer(_rabbitMQClientMock.Object, logger.Object);
        }

        [Fact(Skip = "Verify RabbitMQ configuration")]
        public void InvoiceProducer_ShouldApproved_Success()
        {
            // Arrange
            var approveInvoiceEvent = new Models.Events.ApproveInvoiceEvent {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Currency = string.Empty,
                Date = DateTime.UtcNow,
                Description = string.Empty,
                Discount = 0,
                DueDate = DateTime.UtcNow,
                EventType = Models.Enums.EventType.None,
                HasApproved = true,
                InvoiceFrom = string.Empty,
                InvoiceId = Guid.NewGuid(),
                InvoiceTo = string.Empty,
                Number = 0,
                Price = 0,
                Quantity = 0,
                Tax = 0,
                Total = 0
            };

            // Action
            _invoiceProducer.Approved(approveInvoiceEvent);

            // Assert

        }
    }
}

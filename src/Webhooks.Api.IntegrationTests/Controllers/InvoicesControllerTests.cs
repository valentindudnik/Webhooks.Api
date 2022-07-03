using Webhooks.Api.IntegrationTests.Common;
using Xunit;

namespace Webhooks.Api.IntegrationTests.Controllers
{
    public class InvoicesControllerTests
    {
        private readonly WebhooksApplication _webhooksApplication;

        public InvoicesControllerTests()
        {
            _webhooksApplication = new WebhooksApplication();
        }

        [Fact(Skip = "Need to verify host configuration")]
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

            // Action
            var result = await _webhooksApplication.AddInvoiceAsync(parameters);

            // Assert
            Assert.NotNull(result);
        }
    }
}

using Webhooks.Api.IntegrationTests.Common;

namespace Webhooks.Api.IntegrationTests.Controllers
{
    public class InvoicesControllerTests
    {
        private readonly WebhooksApplication _webhooksApplication;

        public InvoicesControllerTests()
        {
            _webhooksApplication = new WebhooksApplication();
        }
    }
}

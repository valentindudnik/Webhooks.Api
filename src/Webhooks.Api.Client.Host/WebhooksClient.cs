using Webhooks.Api.Client.Host.Interfaces;
using Webhooks.Api.Client.Host.Interfaces.Resources;

namespace Webhooks.Api.Client.Host
{
    public class WebhooksClient : IWebhooksClient
    {
        public WebhooksClient(IInvoiceResource invoiceResource)
        {
            InvoiceResource = invoiceResource;
        }

        public IInvoiceResource InvoiceResource { get; }
    }
}

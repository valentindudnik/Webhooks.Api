using Webhooks.Api.Client.Host.Interfaces.Resources;

namespace Webhooks.Api.Client.Host.Interfaces
{
    public interface IWebhooksClient
    {
        IInvoiceResource InvoiceResource { get; }
    }
}

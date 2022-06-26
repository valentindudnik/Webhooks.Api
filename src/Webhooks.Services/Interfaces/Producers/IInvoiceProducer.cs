using Webhooks.Models.Events;

namespace Webhooks.Services.Interfaces.Producers
{
    public interface IInvoiceProducer
    {
        void Approved(ApproveInvoiceEvent approveInvoiceEvent);
    }
}

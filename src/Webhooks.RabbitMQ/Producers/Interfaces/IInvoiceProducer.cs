using Webhooks.RabbitMQ.Models.Events;

namespace Webhooks.RabbitMQ.Client.Producers.Interfaces
{
    public interface IInvoiceProducer
    {
        void Approved(ApproveInvoiceEvent approveInvoiceEvent);
    }
}

using Microsoft.Extensions.Logging;
using Webhooks.RabbitMQ.Client.Interfaces;
using Webhooks.RabbitMQ.Client.Producers.Interfaces;
using Webhooks.RabbitMQ.Models.Common;
using Webhooks.RabbitMQ.Models.Events;

namespace Webhooks.RabbitMQ.Client.Producers
{
    public class InvoiceProducer : RabbitMQProducer, IInvoiceProducer
    {
        public InvoiceProducer(IRabbitMQClient client, ILogger<InvoiceProducer> logger) : base(client, logger)
        { }

        public void Approved(ApproveInvoiceEvent approveInvoiceEvent)
        {
            Publish(QueueNames.InvoicesQueue, approveInvoiceEvent);
        }
    }
}

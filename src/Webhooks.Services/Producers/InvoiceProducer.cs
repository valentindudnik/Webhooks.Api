using Microsoft.Extensions.Logging;
using Webhooks.RabbitMQ.Client.Interfaces;
using Webhooks.RabbitMQ.Client.Producers;
using Webhooks.RabbitMQ.Models.Common;
using Webhooks.Models.Events;
using Webhooks.Services.Interfaces.Producers;

namespace Webhooks.Services.Producers
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

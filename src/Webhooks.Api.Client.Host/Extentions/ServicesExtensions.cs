using Microsoft.Extensions.DependencyInjection;
using Webhooks.Api.Client.Host.Interfaces;
using Webhooks.Api.Client.Host.Interfaces.Resources;
using Webhooks.Api.Client.Host.Resources;
using Webhooks.Models.Configurations;

namespace Webhooks.Api.Client.Host.Extentions
{
    public static class ServicesExtensions
    {
        public static void ConfigureWebhooksClient(this IServiceCollection services, WebhooksConfigration configuration)
        {
            services.Configure<WebhooksConfigration>(configureOptions => configureOptions = configuration);

            services.AddScoped<IInvoiceResource, InvoiceResource>();
            services.AddScoped<IWebhooksClient, WebhooksClient>();
        }
    }
}

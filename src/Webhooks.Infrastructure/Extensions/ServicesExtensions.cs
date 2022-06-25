using Microsoft.Extensions.DependencyInjection;
using Webhooks.Services;
using Webhooks.Services.Interfaces;

namespace Webhooks.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IInvoiceService, InvoiceService>();
        }
    }
}

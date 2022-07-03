using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using Webhooks.Models.Dtos;
using Webhooks.Models.Parameters;
using Webhooks.Models.Results;

namespace Webhooks.Api.IntegrationTests.Common
{
    internal class WebhooksApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            const string defaultScheme = "Test Scheme";

            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(servicesConfiguration =>
            {
                servicesConfiguration.AddAuthentication(configure =>
                {
                    configure.DefaultAuthenticateScheme = defaultScheme;
                    configure.DefaultChallengeScheme = defaultScheme;
                }).AddTestAuth(configure => { });
            });
        }

        public async Task<EntityResult> AddInvoiceAsync(InvoiceParameters parameters)
        {
            const string requestUri = "/api/v1/invoices";

            EntityResult? result;

            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsJsonAsync(requestUri, parameters);

                if (!response.IsSuccessStatusCode)
                {
                    var errorInfo = await response.Content.ReadFromJsonAsync<ErrorInfoDto>();
                    throw new ApplicationException(errorInfo?.Message);
                }

                result = await response.Content.ReadFromJsonAsync<EntityResult>();
            }

            return result!;
        }

        public async Task<EntityResult> UpdateInvoiceAsync(int invoiceId, InvoiceParameters parameters)
        {
            var requestUri = $"/api/v1/invoices/{invoiceId}";

            EntityResult? result;

            using (var client = CreateHttpClient())
            {
                var response = await client.PutAsJsonAsync(requestUri, parameters);

                if (!response.IsSuccessStatusCode)
                {
                    var errorInfo = await response.Content.ReadFromJsonAsync<ErrorInfoDto>();
                    throw new ApplicationException(errorInfo?.Message);
                }

                result = await response.Content.ReadFromJsonAsync<EntityResult>();
            }

            return result!;
        }

        public async Task DeleteInvoiceAsync(int invoiceId)
        {
            var requestUri = $"/api/v1/invoices/{invoiceId}";

            using (var client = CreateHttpClient())
            {
                var response = await client.DeleteAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorInfo = await response.Content.ReadFromJsonAsync<ErrorInfoDto>();
                    throw new ApplicationException(errorInfo?.Message);
                }
            }
        }

        public async Task<EntityResult> GetInvoiceAsync(int invoiceId)
        {
            var requestUri = $"/api/v1/invoices/{invoiceId}";

            EntityResult? result;

            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorInfo = await response.Content.ReadFromJsonAsync<ErrorInfoDto>();
                    throw new ApplicationException(errorInfo?.Message);
                }

                result = await response.Content.ReadFromJsonAsync<EntityResult>();
            }

            return result!;
        }

        public async Task<IEnumerable<InvoiceDto>> GetInvoicesAsync()
        {
            var requestUri = $"/api/v1/invoices";

            IEnumerable<InvoiceDto> result;

            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    var errorInfo = await response.Content.ReadFromJsonAsync<ErrorInfoDto>();
                    throw new ApplicationException(errorInfo?.Message);
                }

                result = await response.Content.ReadFromJsonAsync<IEnumerable<InvoiceDto>>();
            }

            return result!;
        }

        private HttpClient CreateHttpClient()
        {
            const string baseAddress = "http://localhost";

            var client = Server.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            return client;
        }
    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Webhooks.Api.Client.Host.Interfaces.Resources;
using Webhooks.Models.Configurations;
using Webhooks.Models.Dtos;
using Webhooks.Models.Exceptions;
using Webhooks.Models.Parameters;

namespace Webhooks.Api.Client.Host.Resources
{
    public class InvoiceResource : IInvoiceResource
    {
        private readonly WebhooksConfigration _configuration;

        public InvoiceResource(IOptions<WebhooksConfigration> configurationOptions)
        {
            _configuration = configurationOptions.Value;
        }

        public async Task AddAsync(InvoiceParameters parameters)
        {
            const string requestUri = "/api/v1/Invoices";

            using var client = new RestClient(_configuration.ServiceUrl!);

            var request = new RestRequest(requestUri);
            request.AddBody(parameters);

            var result = await client.PostAsync(request, CancellationToken.None);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvoiceInvalidException(result.ErrorMessage!, result.ErrorException!);
            }
        }

        public async Task UpdateAsync(Guid invoiceId, InvoiceParameters parameters)
        {
            const string requestUri = "/api/v1/Invoices";

            using var client = new RestClient(_configuration.ServiceUrl!);

            var request = new RestRequest(requestUri);
            request.AddBody(parameters);

            var result = await client.PutAsync(request, CancellationToken.None);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvoiceInvalidException(result.ErrorMessage!, result.ErrorException!);
            }
        }

        public async Task DeleteAsync(Guid invoiceId)
        {
            var requestUri = $"/api/v1/Invoices/{invoiceId}";

            using var client = new RestClient(_configuration.ServiceUrl!);

            var request = new RestRequest(requestUri);

            var result = await client.DeleteAsync(request, CancellationToken.None);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvoiceInvalidException(result.ErrorMessage!, result.ErrorException!);
            }
        }

        public async Task<InvoiceDto> GetAsync(Guid invoiceId)
        {
            var requestUri = $"/api/v1/Invoices/{invoiceId}";

            using var client = new RestClient(_configuration.ServiceUrl!);

            var request = new RestRequest(requestUri);

            var response = await client.GetAsync(request, CancellationToken.None);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvoiceInvalidException(response.ErrorMessage!, response.ErrorException!);
            }

            var result = JsonConvert.DeserializeObject<InvoiceDto>(response.Content!);

            return result!;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var requestUri = "/api/v1/Invoices/";

            using var client = new RestClient(_configuration.ServiceUrl!);

            var request = new RestRequest(requestUri);

            var response = await client.GetAsync(request, CancellationToken.None);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvoiceInvalidException(response.ErrorMessage!, response.ErrorException!);
            }

            var result = JsonConvert.DeserializeObject<IEnumerable<InvoiceDto>>(response.Content!);

            return result!;
        }
    }
}

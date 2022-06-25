using Webhooks.Models.Dtos;
using Webhooks.Models.Parameters;

namespace Webhooks.Api.Client.Host.Interfaces.Resources
{
    public interface IInvoiceResource
    {
        Task AddAsync(InvoiceParameters parameters);
        Task UpdateAsync(Guid invoiceId, InvoiceParameters parameters);
        Task DeleteAsync(Guid invoiceId);
        Task<InvoiceDto> GetAsync(Guid invoiceId); 
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
    }
}

using Webhooks.Models.Dtos;
using Webhooks.Models.Parameters;
using Webhooks.Models.Results;

namespace Webhooks.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<EntityResult> AddAsync(InvoiceParameters parameters);
        Task UpdateAsync(Guid invoiceId, InvoiceParameters parameters);
        Task DeleteAsync(Guid invoiceId);
        Task<InvoiceDto> GetAsync(Guid invoiceId);
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task ApproveAsync(Guid invoiceId);
    }
}

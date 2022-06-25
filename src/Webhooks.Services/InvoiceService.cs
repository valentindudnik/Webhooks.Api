using AutoMapper;
using Microsoft.Extensions.Logging;
using Webhooks.DataAccess.Interfaces;
using Webhooks.DataAccess.Models.Entities;
using Webhooks.Models.Dtos;
using Webhooks.Models.Exceptions;
using Webhooks.Models.Parameters;
using Webhooks.RabbitMQ.Client.Producers.Interfaces;
using Webhooks.Services.Interfaces;

namespace Webhooks.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IGenericRepository<Invoice> _repository;
        private readonly IInvoiceProducer _invoiceProducer;

        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IGenericRepository<Invoice> repository, IInvoiceProducer invoiceProducer, IMapper mapper, ILogger<InvoiceService> logger)
        {
            _repository = repository;
            _invoiceProducer = invoiceProducer;

            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAsync(InvoiceParameters parameters)
        {
            _logger.LogInformation($"{nameof(AddAsync)} with parameters.");

            var invoice = _mapper.Map<InvoiceDto>(parameters);

            var entity = _mapper.Map<Invoice>(invoice);

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Invoice)} with Id: {entity.Id} was added successfully.");
        }

        public async Task UpdateAsync(Guid invoiceId, InvoiceParameters parameters)
        {
            _logger.LogInformation($"{nameof(UpdateAsync)} with parameters.");

            var invoice = await _repository.FindAsync(x => x.Id == invoiceId);
            if (invoice == null)
            {
                var message = $"{nameof(Invoice)} with {nameof(invoiceId)}: {invoiceId} not found.";

                _logger.LogError(message);
                throw new InvoiceNotFoundException(message);
            }

            var targetInvoice = _mapper.Map<InvoiceDto>(parameters);

            invoice.Number = targetInvoice.Number;
            invoice.Price = targetInvoice.Price;
            invoice.Total = targetInvoice.Total;
            invoice.Discount = targetInvoice.Discount;
            invoice.Tax = targetInvoice.Tax;
            invoice.Quantity = targetInvoice.Quantity;
            invoice.InvoiceTo = targetInvoice.InvoiceTo;
            invoice.InvoiceFrom = targetInvoice.InvoiceFrom;
            invoice.Currency = targetInvoice.Currency;
            invoice.Description = targetInvoice.Description;
            invoice.Date = targetInvoice.Date;
            invoice.HasApproved = targetInvoice.HasApproved;
            invoice.DueDate = targetInvoice.DueDate;
            invoice.Updated = DateTime.UtcNow;
            
            await _repository.UpdateAsync(invoice);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Invoice)} with Id: {invoiceId} was updated successfully.");
        }

        public async Task DeleteAsync(Guid invoiceId)
        {
            _logger.LogInformation($"{nameof(DeleteAsync)} with {nameof(invoiceId)}: {invoiceId}.");

            var invoice = await _repository.FindAsync(x => x.Id == invoiceId);
            if (invoice == null)
            {
                var message = $"{nameof(Invoice)} with {nameof(invoiceId)}: {invoiceId} not found.";

                _logger.LogError(message);
                throw new InvoiceNotFoundException(message);
            }

            await _repository.DeleteAsync(x => x.Id == invoiceId);
            
            await _repository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(Invoice)} with Id: {invoiceId} was deleted successfully.");
        }

        public async Task<InvoiceDto> GetAsync(Guid invoiceId)
        {
            _logger.LogInformation($"{nameof(GetAsync)} with {nameof(invoiceId)}: {invoiceId}.");

            var invoice = await _repository.FindAsync(x => x.Id == invoiceId);
            if (invoice == null)
            {
                var message = $"{nameof(Invoice)} with {nameof(invoiceId)}: {invoiceId} not found.";

                _logger.LogError(message);
                throw new InvoiceNotFoundException(message);
            }

            var result = _mapper.Map<InvoiceDto>(invoice);

            _logger.LogInformation($"{nameof(Invoice)} with Id: {invoiceId} was getting successfully.");

            return result;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            _logger.LogInformation($"{nameof(GetAllAsync)}.");

            var invoices = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);

            _logger.LogInformation($"Invoices were getting successfully.");

            return result;
        }

        public async Task ApproveAsync(Guid invoiceId)
        {
            _logger.LogInformation($"{nameof(ApproveAsync)} with {nameof(invoiceId)}: {invoiceId}.");

            var invoice = await _repository.FindAsync(x => x.Id == invoiceId);
            if (invoice == null)
            {
                var message = $"{nameof(Invoice)} with {nameof(invoiceId)}: {invoiceId} not found.";

                _logger.LogError(message);
                throw new InvoiceNotFoundException(message);
            }

            invoice.HasApproved = true;
            invoice.Updated = DateTime.UtcNow;

            await _repository.UpdateAsync(invoice);

            await _repository.SaveChangesAsync();

            // Publish event: 'invoice_approved'
            var approveInvoiceEvent = _mapper.Map<RabbitMQ.Models.Events.ApproveInvoiceEvent>(invoice);
            _invoiceProducer.Approved(approveInvoiceEvent);

            _logger.LogInformation($"{nameof(Invoice)} with Id: {invoiceId} was approved successfully.");
        }
    }
}

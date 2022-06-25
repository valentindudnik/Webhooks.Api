using FluentValidation;
using Webhooks.Models.Parameters;

namespace Webhooks.Infrastructure.Validators
{
    public class InvoiceValidator : AbstractValidator<InvoiceParameters>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.Price).NotNull().WithMessage(x => $"{x.Price} is required.");
            RuleFor(x => x.Quantity).NotNull().WithMessage(x => $"{x.Quantity} is required.");
            RuleFor(x => x.Total).NotNull().WithMessage(x => $"{x.Total} is required.");
            RuleFor(x => x.Discount).NotNull().WithMessage(x => $"{x.Discount} is required.");
            RuleFor(x => x.Tax).NotNull().WithMessage(x => $"{x.Tax} is required.");
            RuleFor(x => x.InvoiceTo).NotEmpty().NotNull().WithMessage(x => $"{x.InvoiceTo} is required.");
            RuleFor(x => x.InvoiceFrom).NotEmpty().NotNull().WithMessage(x => $"{x.InvoiceFrom} is required.");
            RuleFor(x => x.Currency).NotEmpty().NotNull().WithMessage(x => $"{x.Currency} is required.");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage(x => $"{x.Description} is required.");
            RuleFor(x => x.Date).NotEmpty().NotNull().WithMessage(x => $"{x.Date} is required.");
            RuleFor(x => x.DueDate).NotEmpty().NotNull().WithMessage(x => $"{x.DueDate} is required.");
        }
    }
}
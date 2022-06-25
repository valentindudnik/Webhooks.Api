using AutoMapper;
using Webhooks.DataAccess.Models.Entities;
using Webhooks.Models.Dtos;
using Webhooks.Models.Parameters;

namespace Webhooks.Infrastructure.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoiceParameters, InvoiceDto>()
                .ForMember(x => x.Number, opt => opt.MapFrom(x => new Random().Next(1, 1000000)))
                .ForMember(x => x.Updated, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<InvoiceDto, Invoice>()
                .ReverseMap();

            CreateMap<Invoice, RabbitMQ.Models.Events.ApproveInvoiceEvent>()
                .ForMember(x => x.InvoiceId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(x => x.Created, opt => opt.MapFrom(x => DateTime.UtcNow));
        }
    }
}

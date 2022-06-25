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
                .ForMember(x => x.Number, opt => opt.MapFrom(x => new Random().Next(1, 13)))
                .ReverseMap();

            CreateMap<InvoiceDto, Invoice>().ReverseMap();
        }
    }
}

using AspCoreSandbox.Data.Entities;
using AspCoreSandbox.ViewModels;
using AutoMapper;

namespace AspCoreSandbox.Data.Mappings
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            // Tworzy mapowanie. Dodatkowo ReverseMap - w drugą stronę. ForMember - meber klasy określony ręcznie
            CreateMap<Order, OrderViewModel>().ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id)).ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}

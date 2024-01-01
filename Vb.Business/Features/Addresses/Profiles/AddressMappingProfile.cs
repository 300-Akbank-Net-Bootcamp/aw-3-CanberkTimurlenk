using AutoMapper;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Profiles;
public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<AddressRequest, Address>();
        CreateMap<Address, AddressResponse>()
            .ForMember(dest => dest.CustomerName,
                src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));
    }
}

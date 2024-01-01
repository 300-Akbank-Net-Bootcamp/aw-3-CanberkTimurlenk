using AutoMapper;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Profiles;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        CreateMap<ContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>()
            .ForMember(dest => dest.CustomerName,
                src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));
    }
}

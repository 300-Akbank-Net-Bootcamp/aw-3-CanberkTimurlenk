using AutoMapper;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Profiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}

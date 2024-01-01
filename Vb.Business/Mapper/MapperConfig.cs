using AutoMapper;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {

        CreateMap<AccountRequest, Account>();
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.CustomerName,
                src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));


        CreateMap<AccountTransactionRequest, AccountTransaction>();
        CreateMap<AccountTransaction, AccountTransactionResponse>();

        CreateMap<EftTransactionRequest, EftTransaction>();
        CreateMap<EftTransaction, EftTransactionResponse>();
    }
}
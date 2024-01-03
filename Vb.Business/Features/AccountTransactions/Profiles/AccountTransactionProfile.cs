using AutoMapper;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Profiles;
public class AccountTransactionProfile : Profile
{
    public AccountTransactionProfile()
    {
        CreateMap<AccountTransactionRequest, AccountTransaction>();
        CreateMap<AccountTransaction, AccountTransactionResponse>();
    }
}
using AutoMapper;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Profiles;
public class EftTransactionProfile : Profile
{
    public EftTransactionProfile()
    {
        CreateMap<EftTransactionRequest, EftTransaction>();
        CreateMap<EftTransaction, EftTransactionResponse>();
    }
}
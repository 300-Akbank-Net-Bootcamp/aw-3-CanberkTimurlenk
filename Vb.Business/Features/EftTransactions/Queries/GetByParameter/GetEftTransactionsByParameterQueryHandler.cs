using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Queries.GetByParameter;
public class GetEftTransactionsByParameterQueryHandler :
    IRequestHandler<GetEftTransactionsByParameterQuery, ApiResponse<List<EftTransactionResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetEftTransactionsByParameterQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetEftTransactionsByParameterQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<EftTransaction>()
            .AsNoTracking() // since the data is fetched for read only purposes
                            // as no tracking is used to improve performance
            .Where(acc =>
                         (request.AccountId.Equals(0) || acc.AccountId.Equals(request.AccountId)) &&
                         (request.ReferenceNumber == null || acc.ReferenceNumber.ToUpper().Contains(request.ReferenceNumber.ToUpper())) &&
                         (request.SenderAccount == null || acc.SenderAccount.ToUpper().Contains(request.SenderAccount.ToUpper())) &&
                         (request.SenderIban == null || acc.SenderIban.ToUpper().Contains(request.SenderIban.ToUpper())) &&
                         (request.SenderName == null || acc.SenderName.ToUpper().Contains(request.SenderName.ToUpper())))
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<EftTransaction>, List<EftTransactionResponse>>(list);
        return new ApiResponse<List<EftTransactionResponse>>(mappedList);
    }
}
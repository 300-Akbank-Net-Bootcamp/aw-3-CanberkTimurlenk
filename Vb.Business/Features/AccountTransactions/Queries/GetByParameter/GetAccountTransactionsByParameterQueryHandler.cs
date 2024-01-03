using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Queries.GetByParameter;
public class GetAccountTransactionsByParameterQueryHandler : IRequestHandler<GetAccountTransactionsByParameterQuery, ApiResponse<List<AccountTransactionResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAccountTransactionsByParameterQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAccountTransactionsByParameterQuery request, CancellationToken cancellationToken)
    {
        var accountTransactions = await dbContext.AccountTransactions
            .AsNoTracking() // since the data is fetched for read only purposes
                            // as no tracking is used to improve performance
            .Where(a =>
            (request.Description == null || a.Description.ToUpper().Contains(request.Description.ToUpper())) &&
            (request.TransferType == null || a.TransferType.ToUpper() == request.TransferType.ToUpper()))
            .ToListAsync(cancellationToken);

        var response = mapper.Map<List<AccountTransactionResponse>>(accountTransactions);

        return new ApiResponse<List<AccountTransactionResponse>>(response);
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Queries.GetAll;
public class GetAllAccountTransactionsQueryHandler :
    IRequestHandler<GetAllAccountTransactionsQuery, ApiResponse<List<AccountTransactionResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllAccountTransactionsQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        var accountTransactions = await dbContext.AccountTransactions
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var response = mapper.Map<List<AccountTransactionResponse>>(accountTransactions);

        return new ApiResponse<List<AccountTransactionResponse>>(response);
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.AccountTransactions.Constants;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Queries.GetById;

public class GetAccountTransactionByIdQueryHandler : IRequestHandler<GetAccountTransactionByIdQuery, ApiResponse<AccountTransactionResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAccountTransactionByIdQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var accountTransaction = await dbContext.AccountTransactions
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

        if (accountTransaction == null)
            return new ApiResponse<AccountTransactionResponse>(AccountTransactionMessages.RecordNotExists);

        var response = mapper.Map<AccountTransactionResponse>(accountTransaction);

        return new ApiResponse<AccountTransactionResponse>(response);
    }
}
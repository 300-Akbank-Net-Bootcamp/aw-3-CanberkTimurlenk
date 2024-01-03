using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Queries.GetAll;
public class GetAllEftTransactionsQueryHandler : IRequestHandler<GetAllEftTransactionsQuery, List<EftTransactionResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllEftTransactionsQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<EftTransactionResponse>> Handle(GetAllEftTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.EftTransactions
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return mapper.Map<List<EftTransactionResponse>>(transactions);
    }
}
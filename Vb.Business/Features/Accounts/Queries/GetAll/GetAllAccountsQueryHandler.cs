using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Queries.GetAll;
public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, ApiResponse<List<AccountResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllAccountsQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountsQuery request,
                   CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>()
            .Include(a => a.AccountTransactions)
            .Include(a => a.EftTransactions)
            .AsNoTracking() // since the data is fetched for read only purposes
                            // as no tracking is used to improve performance
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }
}
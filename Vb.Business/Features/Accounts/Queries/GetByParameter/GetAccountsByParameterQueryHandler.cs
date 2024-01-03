using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Queries.GetByParameter;
public class GetAccountsByParameterQueryHandler : IRequestHandler<GetAccountsByParameterQuery, ApiResponse<List<AccountResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;
    public GetAccountsByParameterQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountsByParameterQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>()
            .Include(acc => acc.AccountTransactions)
            .Include(acc => acc.EftTransactions)
            .AsNoTrackingWithIdentityResolution()
            .Where(acc =>
                        (request.CustomerId.Equals(0) || acc.CustomerId.Equals(request.CustomerId)) &&
                        (request.IBAN == null || acc.IBAN.ToUpper().Contains(request.IBAN.ToUpper())))
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }
}

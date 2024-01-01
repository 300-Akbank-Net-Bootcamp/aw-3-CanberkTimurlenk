using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Queries.GetAll;

public class GetAllCustomerQueryHandler :
IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>
{

    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllCustomerQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Contacts)
            .Include(x => x.Addresses).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }
}
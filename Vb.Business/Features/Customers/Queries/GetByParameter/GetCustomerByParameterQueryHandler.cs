using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Data.Entity;
using Vb.Data;
using Vb.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vb.Business.Features.Customers.Queries.GetByParameter;

public class GetCustomerByParameterQueryHandler :
   IRequestHandler<GetCustomerByParameterQuery, ApiResponse<List<CustomerResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetCustomerByParameterQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Contacts)
            .Include(x => x.Addresses)
            .Where(x =>
                (request.FirstName == null || x.FirstName.ToUpper().Contains(request.FirstName.ToUpper())) &&
                (request.LastName == null || x.LastName.ToUpper().Contains(request.LastName.ToUpper())) &&
                (request.IdentityNumber == null || x.IdentityNumber.ToUpper().Contains(request.IdentityNumber.ToUpper())))
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }
}
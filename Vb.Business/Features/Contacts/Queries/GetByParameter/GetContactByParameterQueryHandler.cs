using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetByParameter;

public class GetContactByParameterQueryHandler : IRequestHandler<GetContactByParameterQuery, ApiResponse<List<ContactResponse>>>
{

    private readonly VbDbContext context;
    private readonly IMapper mapper;

    public GetContactByParameterQueryHandler(VbDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ContactResponse>>> Handle(GetContactByParameterQuery request, CancellationToken cancellationToken)
    {
        var list = await context.Contacts
                            .AsNoTracking() // since the data is fetched for read only purposes
                                            // as no tracking is used to improve performance
                            .Where(c =>
                                (request.CustomerId.Equals(0) || c.CustomerId.Equals(request.CustomerId)) &&
                                // since customerId is an integer, when it is not provided, it will be 0, so customerId will not be filtered
                                (request.ContactType == null || c.ContactType.ToUpper().Contains(request.ContactType.ToUpper())) &&
                                (request.Information == null || c.Information.ToUpper().Contains(request.Information.ToUpper())))
                            .ToListAsync(cancellationToken);

        var response = mapper.Map<List<ContactResponse>>(list);

        return new ApiResponse<List<ContactResponse>>(response);
    }
}

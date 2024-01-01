using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetByParameter;

public class GetContactByParameterQueryHandler : IRequestHandler<GetContactByParameterQuery, ApiResponse<List<ContactResponse>>>
{

    private readonly VbDbContext _context;
    private readonly IMapper _mapper;

    public GetContactByParameterQueryHandler(VbDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ContactResponse>>> Handle(GetContactByParameterQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Contacts
            .Include(c => c.Customer)
            .Where(c =>
                (c.CustomerId.Equals(0) || c.CustomerId.Equals(request.customerId)) &&
                // since customerId is an integer, when it is not provided, it will be 0, so customerId will not be filtered
                (request.contactType == null || c.ContactType.ToUpper().Contains(request.contactType.ToUpper())) &&
                (request.information == null || c.Information.ToUpper().Contains(request.information.ToUpper())))
            .ToListAsync(cancellationToken);

        var response = _mapper.Map<List<ContactResponse>>(list);

        return new ApiResponse<List<ContactResponse>>(response);
    }
}

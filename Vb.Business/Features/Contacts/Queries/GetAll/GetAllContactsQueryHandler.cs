using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetAll;

public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, ApiResponse<List<ContactResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllContactsQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ContactResponse>>> Handle(GetAllContactsQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Contact>()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Contact>, List<ContactResponse>>(list);
        return new ApiResponse<List<ContactResponse>>(mappedList);
    }

}

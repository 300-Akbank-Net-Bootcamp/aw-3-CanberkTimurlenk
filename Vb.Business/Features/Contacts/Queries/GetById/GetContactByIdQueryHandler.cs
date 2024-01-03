using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Contacts.Constants;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetById;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ApiResponse<ContactResponse>>
{
    private readonly VbDbContext _context;
    private readonly IMapper _mapper;

    public GetContactByIdQueryHandler(VbDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ContactResponse>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (contact == null)
            return new ApiResponse<ContactResponse>(ContactMessages.RecordNotExists);

        var response = _mapper.Map<ContactResponse>(contact);

        return new ApiResponse<ContactResponse>(response);
    }
}

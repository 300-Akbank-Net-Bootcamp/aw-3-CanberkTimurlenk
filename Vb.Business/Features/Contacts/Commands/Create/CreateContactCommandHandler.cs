using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Create;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ApiResponse<ContactResponse>>
{
    private readonly VbDbContext context;
    private readonly IMapper mapper;

    public CreateContactCommandHandler(VbDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = mapper.Map<Contact>(request.model);

        await context.Contacts.AddAsync(contact, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ContactResponse>(contact);

        return new ApiResponse<ContactResponse>(response);
    }
}

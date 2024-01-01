using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Delete;
public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ApiResponse>
{
    private readonly VbDbContext context;
    private readonly IMapper mapper;

    public DeleteContactCommandHandler(VbDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await context.Contacts
            .FindAsync(request.id, cancellationToken);

        if (contact == null)
            return new ApiResponse("Contact not found.");

        contact.IsActive = false;
        await context.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ContactResponse>(contact);

        return new ApiResponse();
    }
}

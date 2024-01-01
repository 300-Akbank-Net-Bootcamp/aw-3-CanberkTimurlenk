using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Data;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Update;
public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ApiResponse>
{
    private readonly VbDbContext context;
    private readonly IMapper mapper;

    public UpdateContactCommandHandler(VbDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await context.Contacts
            .FindAsync(request.id, cancellationToken);

        if (contact == null)
            return new ApiResponse("Contact not found.");

        mapper.Map(request.model, contact);

        await context.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ContactResponse>(contact);

        return new ApiResponse();
    }

}

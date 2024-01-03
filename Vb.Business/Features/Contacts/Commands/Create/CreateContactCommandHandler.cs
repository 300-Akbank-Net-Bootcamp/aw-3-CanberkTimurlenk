using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Contacts.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Create;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ApiResponse<ContactResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CreateContactCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        bool isValidToAdd = request.Model.IsDefault
                    ? !(await dbContext.Set<Contact>().AnyAsync(x => x.CustomerId == request.Model.CustomerId && x.IsDefault))
                    : true;

        if (!isValidToAdd)
            return new ApiResponse<ContactResponse>(string.Format(ContactMessages.DefaultContactAlreadyExistsForCustomerId, request.Model.CustomerId));

        var contact = mapper.Map<Contact>(request.Model);

        await dbContext.Contacts.AddAsync(contact, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ContactResponse>(contact);

        return new ApiResponse<ContactResponse>(response);
    }
}

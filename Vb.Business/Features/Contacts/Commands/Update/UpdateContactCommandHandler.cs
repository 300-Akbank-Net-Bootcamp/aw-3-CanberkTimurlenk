using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Contacts.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Update;
public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateContactCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(ContactMessages.RecordNotExists);

        var hasDefaultContact = await dbContext.Set<Contact>().AnyAsync(c => c.IsDefault && c.CustomerId == request.Id, cancellationToken);
        // true; if the customer already has a default contact

        if (hasDefaultContact && request.Model.IsDefault)
            return new ApiResponse(string.Format(ContactMessages.DefaultContactAlreadyExistsForCustomerId, request.Id));
        // if the customer already has a default contact and the request model is default,
        // just returns a message

        fromdb.Information = request.Model.Information;
        fromdb.ContactType = request.Model.ContactType;

        if (!(hasDefaultContact || request.Model.IsDefault))
            fromdb.IsDefault = true;
        // if the customer doesn't have a default contact and the request model is not default,
        // set the current contact as default
        // this is to ensure that there is always a default contact for a customer        

        else
            fromdb.IsDefault = request.Model.IsDefault;

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ContactResponse>(fromdb);

        return new ApiResponse();
    }

}

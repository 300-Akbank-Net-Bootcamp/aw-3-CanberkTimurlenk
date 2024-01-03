using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Create;

public record CreateContactCommand(ContactRequest Model) : IRequest<ApiResponse<ContactResponse>>;
using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Update;
public record UpdateContactCommand(int id, ContactRequest model) : IRequest<ApiResponse>;

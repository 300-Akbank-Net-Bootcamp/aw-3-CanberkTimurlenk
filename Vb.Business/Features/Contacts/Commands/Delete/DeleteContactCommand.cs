using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Delete;
public record DeleteContactCommand(int Id) : IRequest<ApiResponse>;
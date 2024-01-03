using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Commands.Update;
public record UpdateAccountCommand(int AccountNumber, AccountRequest Model) : IRequest<ApiResponse>;
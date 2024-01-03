using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Commands.Create;
public record CreateAccountCommand(AccountRequest Model) : IRequest<ApiResponse<AccountResponse>>;
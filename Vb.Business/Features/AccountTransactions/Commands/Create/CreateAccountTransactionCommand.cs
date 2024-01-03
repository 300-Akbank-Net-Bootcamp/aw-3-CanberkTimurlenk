using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Commands.Create;
public record CreateAccountTransactionCommand(AccountTransactionRequest Model) : IRequest<ApiResponse<AccountTransactionResponse>>;
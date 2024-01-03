using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Commands.Update;
public record UpdateAccountTransactionCommand(int Id, AccountTransactionRequest Model) : IRequest<ApiResponse>;
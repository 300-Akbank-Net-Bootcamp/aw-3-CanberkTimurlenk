using MediatR;
using Vb.Base.Response;

namespace Vb.Business.Features.AccountTransactions.Commands.Delete;
public record DeleteAccountTransactionCommand(int Id) : IRequest<ApiResponse>;
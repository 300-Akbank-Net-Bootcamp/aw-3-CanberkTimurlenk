using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Commands.Create;
public record CreateEftTransactionCommand(EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
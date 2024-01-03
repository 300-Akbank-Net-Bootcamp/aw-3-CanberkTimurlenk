using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Commands.Update;
public record UpdateEftTransactionCommand(int Id, EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
using MediatR;
using Vb.Base.Response;

namespace Vb.Business.Features.EftTransactions.Commands.Delete;
public record DeleteEftTransactionCommand(int Id) : IRequest<ApiResponse>;

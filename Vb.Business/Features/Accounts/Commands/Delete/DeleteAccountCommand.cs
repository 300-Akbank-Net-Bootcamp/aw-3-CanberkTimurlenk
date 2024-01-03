using MediatR;
using Vb.Base.Response;

namespace Vb.Business.Features.Accounts.Commands.Delete;
public record DeleteAccountCommand(int AccountNumber) : IRequest<ApiResponse>;
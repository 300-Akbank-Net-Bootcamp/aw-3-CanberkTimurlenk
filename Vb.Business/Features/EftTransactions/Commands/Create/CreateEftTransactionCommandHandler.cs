using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.EftTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Commands.Create;
public class CreateEftTransactionCommandHandler : IRequestHandler<CreateEftTransactionCommand, ApiResponse<EftTransactionResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CreateEftTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(CreateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!CheckRequestIsValid(request.Model, out var errorMessage)) // early return if request is invalid
            return new ApiResponse<EftTransactionResponse>(errorMessage);

        var eftTransaction = mapper.Map<EftTransaction>(request.Model);

        await dbContext.EftTransactions.AddAsync(eftTransaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EftTransactionResponse>(eftTransaction);

        return new ApiResponse<EftTransactionResponse>(response);
    }

    /// <summary>
    /// Validates a create EFT transaction request.
    /// </summary>
    /// <param name="request">The EFT transaction request to be validated.</param>
    /// <param name="errorMessage">
    /// If the request is invalid, this parameter will contain one or more eror messages;
    /// otherwise, it will be empty string.
    /// </param>
    /// <returns>
    /// Returns true if the request is valid; otherwise, returns false.
    /// </returns>
    private bool CheckRequestIsValid(EftTransactionRequest request, out string? errorMessage)
    {
        List<string> errorMessages = new();

        if (request.TransactionDate < DateTime.UtcNow.AddMinutes(-2)) // allow 2 minute delay, to test
            errorMessages.Add(EftTransactionMessages.TransactionDateInThePastNotAllowed);

        if (errorMessages.Count > 0)
        {
            errorMessage = string.Join("\n", errorMessages);
            // when using json parse, \n could be recognized as new line
            // or, in ApiResponse the property "Messages" could be changed as collection of string
            return false;
        }

        else
        {
            errorMessage = string.Empty;
            return true;
        }
    }

}

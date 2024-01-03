using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.AccountTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.AccountTransactions.Commands.Update;
public class UpdateAccountTransactionCommandHandler :
    IRequestHandler<UpdateAccountTransactionCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateAccountTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<AccountTransaction>()
            .FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(AccountTransactionMessages.RecordNotExists);

        if (!CheckRequestIsValid(request, fromdb, out var errorMessage))
            return new ApiResponse(errorMessage);

        fromdb.Amount = request.Model.Amount;
        fromdb.Description = request.Model.Description;
        fromdb.TransactionDate = request.Model.TransactionDate;
        fromdb.TransferType = request.Model.TransferType;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }

    /// <summary>
    /// Validates an update request for an Account transaction.
    /// </summary>
    /// <param name="request">The update account transaction request.</param>
    /// <param name="existedTransaction">The existing transaction to be updated.</param>
    /// <param name="errorMessage">
    /// If the request is invalid, this parameter will contain
    /// one or more error messages; otherwise, it will be an empty string.
    /// </param>
    /// <returns>
    /// Returns true if the request is valid; otherwise, returns false.
    /// </returns>
    private bool CheckRequestIsValid(UpdateAccountTransactionCommand request, AccountTransaction existedTransaction,
        out string? errorMessage)
    {
        // since the implementation completely consist of business logic, fluent validation were not used

        List<string> errorMessages = new();

        if (existedTransaction.TransactionDate < DateTime.Now)
            errorMessages.Add(AccountTransactionMessages.ModificationNotAllowedForCompletedTransaction);

        if (request.Model.TransactionDate < DateTime.Now)
            errorMessages.Add(AccountTransactionMessages.TransactionDateInThePastNotAllowed);

        if (errorMessages.Any())
        {
            errorMessage = string.Join("\n", errorMessages);
            // when using json parse, \n could be recognized as new line
            // or, in ApiResponse the property "Messages" could be changed as collection of string
            return false;
        }

        errorMessage = null;
        return true;
    }
}

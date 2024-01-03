using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.EftTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Commands.Update;
public class UpdateEftTransactionCommandHandler :
    IRequestHandler<UpdateEftTransactionCommand, ApiResponse<EftTransactionResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateEftTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(UpdateEftTransactionCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<EftTransaction>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse<EftTransactionResponse>(EftTransactionMessages.RecordNotExists);

        if (!CheckRequestIsValid(request.Model, fromdb, out var errorMessage)) // early return if request is invalid
            return new ApiResponse<EftTransactionResponse>(errorMessage);

        fromdb.TransactionDate = request.Model.TransactionDate;
        fromdb.Amount = request.Model.Amount;
        fromdb.Description = request.Model.Description;

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EftTransactionResponse>(fromdb);

        return new ApiResponse<EftTransactionResponse>(response);
    }

    /// <summary>
    /// Validates an update request for an EFT transaction.
    /// </summary>
    /// <param name="request">The update EFT transaction request.</param>
    /// <param name="existedTransaction">The existing transaction to be updated.</param>
    /// <param name="errorMessage">
    /// If the request is invalid, this parameter will contain
    /// one or more error messages; otherwise, it will be an empty string.
    /// </param>
    /// <returns>
    /// Returns true if the request is valid; otherwise, returns false.
    /// </returns>
    private bool CheckRequestIsValid(EftTransactionRequest request, EftTransaction existedTransaction,
        out string? errorMessage)
    {
        // since the implementation completely consist of business logic, fluent validation were not used

        List<string> errorMessages = new();

        if (existedTransaction.TransactionDate < DateTime.Now)
            errorMessages.Add(EftTransactionMessages.ModificationNotAllowedForCompletedTransaction);

        if (request.TransactionDate <= DateTime.Now)
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
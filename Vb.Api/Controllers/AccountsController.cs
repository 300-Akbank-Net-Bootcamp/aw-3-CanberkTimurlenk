using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Features.Accounts.Commands.Create;
using Vb.Business.Features.Accounts.Commands.Delete;
using Vb.Business.Features.Accounts.Commands.Update;
using Vb.Business.Features.Accounts.Queries.GetById;
using Vb.Business.Features.Accounts.Queries.GetByParameter;
using Vb.Schema;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;
        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<AccountResponse>>> GetAccountByParameter(int customerId, string? IBAN)
        {
            var operation = new GetAccountsByParameterQuery(customerId, IBAN);
            var result = await mediator.Send(operation);
            return result;
        }

        // All Accounts could also be retrieved by using GetAccountByParameter
        //[HttpGet]
        //public async Task<ApiResponse<List<AccountResponse>>> Get()
        //{
        //    var operation = new GetAllAccountsQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        [HttpGet("{account-number}")]
        public async Task<ApiResponse<AccountResponse>> Get([FromRoute(Name = "account-number")] int accountNumber)
        {
            var operation = new GetAccountByIdQuery(accountNumber);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest account)
        {
            var operation = new CreateAccountCommand(account);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{account-number}")]
        public async Task<ApiResponse> Put([FromRoute(Name = "account-number")] int accountNumber, [FromBody] AccountRequest account)
        {
            var operation = new UpdateAccountCommand(accountNumber, account);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{account-number}")]
        public async Task<ApiResponse> Delete([FromRoute(Name = "account-number")] int accountNumber)
        {
            var operation = new DeleteAccountCommand(accountNumber);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}

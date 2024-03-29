using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Features.Customers.Commands.Create;
using Vb.Business.Features.Customers.Commands.Delete;
using Vb.Business.Features.Customers.Commands.Update;
using Vb.Business.Features.Customers.Queries.GetById;
using Vb.Business.Features.Customers.Queries.GetByParameter;
using Vb.Schema;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator mediator;
    public CustomersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    // since we will be using the same endpoint for all 3 parameters, we will use the same endpoint
    [HttpGet]
    public async Task<ApiResponse<List<CustomerResponse>>> GetCustomersByParameter(string? firstName, string? lastName, string? identityNumber)
    {
        var operation = new GetCustomerByParameterQuery(firstName, lastName, identityNumber);
        var result = await mediator.Send(operation);
        return result;
    }

    //[HttpGet]
    //public async Task<ApiResponse<List<CustomerResponse>>> Get()
    //{
    //    var operation = new GetAllCustomerQuery();
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    [HttpGet("{customer-number}")]
    public async Task<ApiResponse<CustomerResponse>> Get([FromRoute(Name = "customer-number")] int customerNumber)
    {
        var operation = new GetCustomerByIdQuery(customerNumber);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest customer)
    {
        var operation = new CreateCustomerCommand(customer);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{customer-number}")]
    public async Task<ApiResponse> Put([FromRoute(Name = "customer-number")] int customerNumber, [FromBody] CustomerRequest customer)
    {
        var operation = new UpdateCustomerCommand(customerNumber, customer);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{customer-number}")]
    public async Task<ApiResponse> Delete([FromRoute(Name = "customer-number")] int customerNumber)
    {
        var operation = new DeleteCustomerCommand(customerNumber);
        var result = await mediator.Send(operation);
        return result;
    }
}
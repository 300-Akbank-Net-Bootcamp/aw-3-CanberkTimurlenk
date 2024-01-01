using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Features.Contacts.Commands.Create;
using Vb.Business.Features.Contacts.Commands.Delete;
using Vb.Business.Features.Contacts.Commands.Update;
using Vb.Business.Features.Contacts.Queries.GetById;
using Vb.Business.Features.Contacts.Queries.GetByParameter;
using Vb.Schema;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ContactResponse>>> GetByParameter(int customerId, string? contactType, string? information)
        {
            var operation = new GetContactByParameterQuery(customerId, contactType, information);
            var result = await mediator.Send(operation);
            return result;
        }

        //[HttpGet]
        //public async Task<ApiResponse<List<ContactResponse>>> Get()
        //{
        //    var operation = new GetAllContactsQuery();
        //    var result = await mediator.Send(operation);
        //    return result;
        //}

        [HttpGet("{id}")]
        public async Task<ApiResponse<ContactResponse>> Get(int id)
        {
            var operation = new GetContactByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<ContactResponse>> Post([FromBody] ContactRequest request)
        {
            var operation = new CreateContactCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] ContactRequest request)
        {
            var operation = new UpdateContactCommand(id, request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteContactCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}

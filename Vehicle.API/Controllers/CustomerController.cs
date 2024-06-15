using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Application.Features.Customers.Queries;

namespace Vehicle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "ListCustomers")]
        [ProducesResponseType(typeof(IEnumerable<CustomerVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CustomerVM>>> getAll()
        {
            var query = new ListCustomersQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var command = new DeleteCustomerCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut(Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            var result =  await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost(Name = "AddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddCustomer([FromBody] CreateCustomerCommand command)
        {
            var Results = await _mediator.Send(command);
            return Ok(Results);
        }
    }
}

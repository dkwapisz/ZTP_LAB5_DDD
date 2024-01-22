using System.Net;
using Eshop.Application.Customers.Commands;
using Eshop.Application.Customers.Queries;
using Eshop.Application.Orders.CustomerOrder.Commands;
using Eshop.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.API.Controllers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds a new order for a specified customer.
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <param name="request">List of products.</param>
        [Route("{customerId}/orders")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCustomerOrder(
            [FromRoute] Guid customerId,
            [FromBody] CustomerOrderRequest request)
        {
            var response = await _mediator.Send(new AddOrderCommand(customerId, request.Products));
            return Created(string.Empty, response);
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="request">Customer details</param>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCustomer(
            [FromBody] CustomerDto request)
        {
            Guid response = await _mediator.Send(new CreateCustomerCommand(request.Name));
            return Created("api/v1/customers/" + response, response);
        }

        /// <summary>
        /// Retrieve customer.
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        [Route("{customerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerDetails([FromRoute] Guid customerId)
        {
            var customerDetails = await _mediator.Send(new GetCustomerQuery(customerId));
            return Ok(customerDetails);
        }
    }
}

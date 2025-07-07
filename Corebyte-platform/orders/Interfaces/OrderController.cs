namespace Corebyte_platform.orders.Interfaces
{
    using Corebyte_platform.orders.Domain.Services;
    using Corebyte_platform.orders.Interfaces.REST.Resources;
    using Corebyte_platform.orders.Interfaces.REST.Transform;
    using Corebyte_platform.orders.Domain.Model.Queries;
    using Corebyte_platform.orders.Domain.Exceptions;
    using Corebyte_platform.orders.Domain.Model.Commands;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// Order controller
    /// </summary>
    /// <param name="favoriteSourceCommandService">The Order Command Service</param>
    /// <param name="favoriteSourceQueryService">The Order Query Service</param>"
    /// 

    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Orders")]
    public class OrderController(IOrderCommandService orderCommandService, IOrderQueryService orderQueryService) : ControllerBase
    {


        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all orders.",
            Description = "Retrieves a list of all orders in the system.",
            OperationId = "GetAllOrders")]
        [SwaggerResponse(200, "The order records were found", typeof(IEnumerable<OrderResource>))]
        [SwaggerResponse(204, "No Order records found")]

        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();
            var orders = await orderQueryService.Handle(query);
            if (!orders.Any())
            
                return NoContent();

                var resources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
                return Ok(resources);
            
        }

            // POST: Create Order
            [HttpPost]
            [SwaggerOperation(
                Summary = "Creates an order",
                Description = "Creates a new order with customer, product, quantity, total, etc.",
                OperationId = "CreateOrder")]
            [SwaggerResponse(201, "The order was created", typeof(OrderResource))]
            [SwaggerResponse(400, "The order was not created")]
            [SwaggerResponse(409, "An order with the same details already exists")]
            public async Task<ActionResult> CreateOrder([FromBody] CreateOrderResource resource)
            {
                try
                {
                    var createOrderCommand = CreateOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
                    var result = await orderCommandService.Handle(createOrderCommand);
                    return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, OrderResourceFromEntityAssembler.ToResourceFromEntity(result));
                }
                catch (DuplicateOrderException ex)
                {
                    return Conflict(new { message = ex.Message });
                }
                catch (Exception)
                {
                    return BadRequest("The order was not created");
                }
            }



        /// <summary>
        /// Gets an order by customer.
        /// </summary>
        /// <param name="customer">The customer from the news service provider</param>
        /// <returns>
        /// A response as an action result containing the order resource if found, or a not found message if not found.
        /// </returns>
        /// 

        [HttpGet("customer/{customer}")]
        [SwaggerOperation(
            Summary = "Gets an order by customer",
            Description = "Retrieves an order for a specific customer.",
            OperationId = "GetOrderByCustomer")]
        [SwaggerResponse(200, "The order was found", typeof(IEnumerable<OrderResource>))]
        [SwaggerResponse(404, "No orders found for the specified customer")]
        private async Task<ActionResult> GetOrderByCustomer(string customer)
        {
            var query = new GetOrderByCustomerQuery(customer);
            var orders = await orderQueryService.Handle(query);
            if (!orders.Any())
            {
                return NotFound(new { message = "No orders found for the specified customer" });
            }
            var resources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }



        // DELETE: Delete Order by ID
        /// <summary>
        /// Deletes all order for a specific order ID.
        /// </summary>
        /// <param name="id">The idname to delete</param>
        /// <returns>
        /// 
        [HttpDelete("{id}")]
[SwaggerOperation(
    Summary = "Deletes an order by ID",
    Description = "Deletes the specified order if it exists",
    OperationId = "DeleteOrderById")]
[SwaggerResponse(200, "Order was successfully deleted", typeof(OrderResource))]
[SwaggerResponse(404, "No order found for the ID")]
public async Task<IActionResult> DeleteOrders(int id)
{
    try
    {
        var deleteOrderCommand = new DeleteOrdersByIdCommand(id);
        var order = await orderCommandService.Handle(deleteOrderCommand);
        if (order is null)
        {
            return NotFound(new { message = "No order found with the specified ID" });
        }
        var orderResource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        return Ok(orderResource);
    }
    catch (Exception)
    {
        return StatusCode(500, new { message = "An error occurred while deleting the order" });
    }
}


        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="id">The id of the order to get</param>
        /// <returns> 
        /// A response as an action result containing the order resource if found, or a not found message if not found.
        /// </returns>
        /// 
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets an order by ID",
            Description = "Retrieves an order by its ID.",
            OperationId = "GetOrderById")]
        [SwaggerResponse(200, "The order was found", typeof(OrderResource))]
        [SwaggerResponse(404, "No order found with the specified ID")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            var getOrderByIdQuery = new GetOrderByIdQuery(id);
            var result = await orderQueryService.Handle(getOrderByIdQuery);
            if (result is null)
            {
                return NotFound("The Order was not found");
            }
            var resource = OrderResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }

        /// <summary>
        /// Gets an order by product.
        /// </summary>
        /// <param name="product">The product from the news service provider</param>   
        /// <returns>
        /// A response as an action result containing the order resource if found, or a not found message if not found.
        /// </returns>
        /// 
        private async Task<ActionResult> GetOrderByProduct(string product)
        {
            var OrderByProductQuery = new GetOrderByProductQuery(product);
            var order = await orderQueryService.Handle(OrderByProductQuery);
            if (order is null)
            {
                return NotFound("No order found for the specified product" );
            }
            var resource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
            return Ok(resource);


        }
       

        /// <summary>
        /// Gets an order by amount and total.
        /// </summary>
        /// <param name="amount">The amount from the newa service provider </param>
        /// <param name="total">The total from the news service provide</param>
        /// <returns>
        /// A response as an action result containing the order resource if found, or a not found message if not found.
        /// </returns>
        private async Task<ActionResult> GetOrderByAmountAndTotal(int amount, double total)
        {
            var GetOrderByAmountAndTotalQuery = new GetOrderByAmountAndTotalQuery(amount, total);
            var order = await orderQueryService.Handle(GetOrderByAmountAndTotalQuery);
            if (order is null)
            {
                return NotFound("No order found for the specified amount and total");
            }
            var resource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
            return Ok(resource);
        }
    }
}

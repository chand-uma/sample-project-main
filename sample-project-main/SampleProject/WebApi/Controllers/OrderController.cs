using BusinessEntities;
using Core.Services.Orders;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models.Order;

namespace WebApi.Controllers
{
    /// <summary>
    /// API controller for managing Order entities.
    /// </summary>
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderService">The order service dependency.</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="model">The order data from the request body.</param>
        /// <returns>HTTP response with the created order or a conflict if it already exists.</returns>
        [Route("{orderId:guid}/create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var existingOrder = await _orderService.GetOrderAsync(orderId);
            if (existingOrder != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, "Order already exists.");
            }

            var order = await _orderService.CreateAsync(orderId, model.CustomerName, model.ProductId, model.Quantity, model.OrderDate);
            return Found(new OrderData(order));
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="model">The updated order data from the request body.</param>
        /// <returns>HTTP response with the updated order or not found if it does not exist.</returns>
        [Route("{orderId:guid}/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            await _orderService.UpdateAsync(order, model.CustomerName, model.ProductId, model.Quantity, model.OrderDate);
            return Found(new OrderData(order));
        }

        /// <summary>
        /// Deletes an order by its unique identifier.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteOrder(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            await _orderService.DeleteAsync(order);
            return Found();
        }

        /// <summary>
        /// Retrieves an order by its unique identifier.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <returns>HTTP response with the order data or not found if it does not exist.</returns>
        [Route("{orderId:guid}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrder(Guid orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            return Found(new OrderData(order));
        }

        /// <summary>
        /// Retrieves a list of orders with optional filters.
        /// </summary>
        /// <param name="customerName">Optional customer name filter.</param>
        /// <param name="startDate">Optional start date filter.</param>
        /// <param name="endDate">Optional end date filter.</param>
        /// <returns>HTTP response with the filtered list of orders.</returns>
        [Route("list")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrders(string customerName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var orders = (await _orderService.GetOrdersAsync(customerName, startDate, endDate))
                .Select(o => new OrderData(o))
                .ToList();
            return Found(orders);
        }
    }
}
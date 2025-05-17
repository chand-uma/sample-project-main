using BusinessEntities;
using System;

namespace WebApi.Models.Order
{
    /// <summary>
    /// Data transfer object for the Order entity, used to expose order data via the API.
    /// Inherits from IdObjectData to include the order's unique identifier.
    /// </summary>
    public class OrderData : IdObjectData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderData"/> class from an Order entity.
        /// </summary>
        /// <param name="order">The order entity to map.</param>
        public OrderData(BusinessEntities.Order order) : base(order)
        {
            CustomerName = order.CustomerName;
            ProductId = order.ProductId;
            Quantity = order.Quantity;
            OrderDate = order.OrderDate;
        }

        /// <summary>
        /// Gets or sets the name of the customer who placed the order.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product ordered.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Order
{
    /// <summary>
    /// Model used for creating or updating Order entities via the API.
    /// </summary>
    public class OrderModel 
    {
        /// <summary>
        /// Gets or sets the unique identifier of the order.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the customer name of the order.
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Gets or sets the product Id of the order.
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Gets or sets the quantity of the order in the order.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the order date of the order.
        /// </summary>
        public DateTime OrderDate { get; set; }
    }
}
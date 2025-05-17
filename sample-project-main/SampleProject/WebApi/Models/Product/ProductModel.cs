using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Product
{
    /// <summary>
    /// Model used for creating or updating Product entities via the API.
    /// Represents the data sent from the client to the server.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available stock for the product.
        /// </summary>
        public int Stock { get; set; }
    }
}
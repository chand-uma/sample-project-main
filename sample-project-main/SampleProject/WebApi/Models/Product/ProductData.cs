using BusinessEntities;
using System;

namespace WebApi.Models.Product
{
    /// <summary>
    /// Data transfer object for Product entity, used to expose product data via the API.
    /// Inherits from IdObjectData to include the product's unique identifier.
    /// </summary>
    public class ProductData : IdObjectData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductData"/> class from a Product entity.
        /// </summary>
        /// <param name="product">The product entity to map.</param>
        public ProductData(BusinessEntities.Product product) : base(product)
        {
            Name = product.Name;
            Price = product.Price;
            Stock = product.Stock;
        }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available stock for the product.
        /// </summary>
        public int Stock { get; set; }
    }
}
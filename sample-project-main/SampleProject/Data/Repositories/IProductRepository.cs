using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace Data.Repositories
{
    /// <summary>
    /// Repository interface for managing Product entities.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        Task<Product> GetAsync(Guid id);

        /// <summary>
        /// Retrieves a list of products filtered by name and/or price range.
        /// </summary>
        /// <param name="nameFilter">name filter.</param>
        /// <param name="minPrice">Optional minimum price filter (can be null).</param>
        /// <param name="maxPrice">Optional maximum price filter (can be null).</param>
        /// <returns>A collection of products matching the filter criteria.</returns>
        Task<IEnumerable<Product>> GetListAsync(string nameFilter, decimal? minPrice, decimal? maxPrice);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        Task CreateAsync(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        Task UpdateAsync(Product product);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        Task DeleteAsync(Product product);
    }
}
using BusinessEntities;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    /// <summary>
    /// In-memory repository implementation for managing Product entities.
    /// </summary>
    public class ProductRepository : InMemoryRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Retrieves a list of products filtered by name and/or price range.
        /// </summary>
        /// <param name="nameFilter">Name filter.</param>
        /// <param name="minPrice">Optional minimum price filter (can be null).</param>
        /// <param name="maxPrice">Optional maximum price filter (can be null).</param>
        /// <returns>A collection of products matching the filter criteria.</returns>
        public async Task<IEnumerable<Product>> GetListAsync(string nameFilter, decimal? minPrice, decimal? maxPrice)
        {
            // Retrieve all products asynchronously
            var products = await GetAllAsync();

            // Check if products is null or empty
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<Product>(); // Return an empty collection if no products are found
            }

            // Apply filters
            if (!string.IsNullOrEmpty(nameFilter))
            {
                products = products.Where(p => p.Name != null && p.Name.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            return products.ToList(); 
        }

    }
}
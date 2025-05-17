using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace Core.Services.Products
{
    /// <summary>
    /// Service interface for managing Product entities.
    /// </summary>
    public interface IProductService
    {
        Task<Product> CreateAsync(Guid id, string name, decimal price, int stock);
        Task UpdateAsync(Product product, string name, decimal price, int stock);
        Task DeleteAsync(Product product);
        Task<Product> GetProductAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsAsync(string name = null, decimal? minPrice = null, decimal? maxPrice = null);
    }
}

using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    /// <summary>
    /// Service implementation for managing Product entities.
    /// </summary>
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IIdObjectFactory<Product> _productFactory;

        public ProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Guid id, string name, decimal price, int stock)
        {
            var product = _productFactory.Create(id);
            product.SetName(name);
            product.SetPrice(price);
            product.SetStock(stock);
            await _productRepository.CreateAsync(product);
            return product;
        }

        public async Task UpdateAsync(Product product, string name, decimal price, int stock)
        {
            product.SetName(name);
            product.SetPrice(price);
            product.SetStock(stock);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await _productRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            return await _productRepository.GetListAsync(name, minPrice, maxPrice);
        }
    }
}

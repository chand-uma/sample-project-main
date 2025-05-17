using System;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Services.Products;
using WebApi.Models.Product;

namespace WebApi.Controllers
{
    /// <summary>
    /// API controller for managing Product entities.
    /// </summary>
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service dependency.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="model">The product data from the request body.</param>
        /// <returns>HTTP response with the created product or a conflict if it already exists.</returns>
        [Route("{productId:guid}/create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var existingProduct = await _productService.GetProductAsync(productId);
            if (existingProduct != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, "Product already exists.");
            }

            var product = await _productService.CreateAsync(productId, model.Name, model.Price, model.Stock);
            return Found(new ProductData(product));
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="model">The updated product data from the request body.</param>
        /// <returns>HTTP response with the updated product or not found if it does not exist.</returns>
        [Route("{productId:guid}/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = await _productService.GetProductAsync(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            await _productService.UpdateAsync(product, model.Name, model.Price, model.Stock);
            return Found(new ProductData(product));
        }

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteProduct(Guid productId)
        {
            var product = await _productService.GetProductAsync(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            await _productService.DeleteAsync(product);
            return Found();
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <returns>HTTP response with the product data or not found if it does not exist.</returns>
        [Route("{productId:guid}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetProduct(Guid productId)
        {
            var product = await _productService.GetProductAsync(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            return Found(new ProductData(product));
        }

        /// <summary>
        /// Retrieves a paged list of products with optional filters.
        /// </summary>
        /// <param name="name">Optional name filter.</param>
        /// <param name="minPrice">Optional minimum price filter.</param>
        /// <param name="maxPrice">Optional maximum price filter.</param>
        /// <returns>HTTP response with the filtered and paged list of products.</returns>
        [Route("list")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetProducts(string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var products = (await _productService.GetProductsAsync(name, minPrice, maxPrice)).Select(p => new ProductData(p)).ToList();
            return Found(products);
        }

    }
}
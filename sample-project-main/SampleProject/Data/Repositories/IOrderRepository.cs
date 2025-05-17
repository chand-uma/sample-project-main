using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Repository interface for managing Order entities.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves an order by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        /// <returns>The order if found; otherwise, null.</returns>
        Task<Order> GetAsync(Guid id);

        /// <summary>
        /// Retrieves a list of orders filtered by customer name and/or date range.
        /// </summary>
        /// <param name="customerNameFilter">customer name filter.</param>
        /// <param name="startDate">Optional start date for filtering orders (can be null).</param>
        /// <param name="endDate">Optional end date for filtering orders (can be null).</param>
        /// <returns>A collection of orders matching the filter criteria.</returns>
        Task<IEnumerable<Order>> GetListAsync(string customerNameFilter, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order to create.</param>
        Task CreateAsync(Order order);

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        Task UpdateAsync(Order order);

        /// <summary>
        /// Deletes an order .
        /// </summary>
        /// <param name="order">The order to delete.</param>
        Task DeleteAsync(Order order);
    }
}
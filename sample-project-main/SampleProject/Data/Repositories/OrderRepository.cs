using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    /// <summary>
    /// In-memory repository implementation for managing Order entities.
    /// </summary>
    public class OrderRepository : InMemoryRepository<Order>, IOrderRepository
    {
        /// <summary>
        /// Retrieves a list of orders filtered by customer name and/or date range.
        /// </summary>
        /// <param name="customerNameFilter">customer name filter.</param>
        /// <param name="startDate">Optional start date for filtering orders (can be null).</param>
        /// <param name="endDate">Optional end date for filtering orders (can be null).</param>
        /// <returns>A collection of orders matching the filter criteria.</returns>
        public async Task<IEnumerable<Order>> GetListAsync(string customerNameFilter, DateTime? startDate, DateTime? endDate)
        {
            var orders = await GetAllAsync();
            return orders.Where(o =>
                (string.IsNullOrEmpty(customerNameFilter) || o.CustomerName.IndexOf(customerNameFilter, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (!startDate.HasValue || o.OrderDate >= startDate.Value) &&
                (!endDate.HasValue || o.OrderDate <= endDate.Value));
        }
    }
}
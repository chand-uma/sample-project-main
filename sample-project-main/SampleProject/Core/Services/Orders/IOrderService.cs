using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    /// <summary>
    /// Service interface for managing Order entities.
    /// </summary>
    public interface IOrderService
    {
        Task<Order> CreateAsync(Guid id, string customerName, Guid productId, int quantity, DateTime orderDate);
        Task UpdateAsync(Order order, string customerName, Guid productId, int quantity, DateTime orderDate);
        Task DeleteAsync(Order order);
        Task<Order> GetOrderAsync(Guid id);
        Task<IEnumerable<Order>> GetOrdersAsync(string customerName = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}

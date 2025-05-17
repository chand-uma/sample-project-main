using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    /// <summary>
    /// Service implementation for managing Order entities.
    /// </summary>
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IIdObjectFactory<Order> _orderFactory;

        public OrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateAsync(Guid id, string customerName, Guid productId, int quantity, DateTime orderDate)
        {
            var order = _orderFactory.Create(id);
            order.SetCustomerName(customerName);
            order.SetProductId(productId);
            order.SetQuantity(quantity);
            order.SetOrderDate(orderDate);
            await _orderRepository.CreateAsync(order);
            return order;
        }

        public async Task UpdateAsync(Order order, string customerName, Guid productId, int quantity, DateTime orderDate)
        {
            order.SetCustomerName(customerName);
            order.SetProductId(productId);
            order.SetQuantity(quantity);
            order.SetOrderDate(orderDate);
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            await _orderRepository.DeleteAsync(order);
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await _orderRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string customerName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _orderRepository.GetListAsync(customerName, startDate, endDate);
        }
    }
}

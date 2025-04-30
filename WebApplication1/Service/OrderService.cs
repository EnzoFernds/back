using System;
using System.Collections.Generic;

namespace RestaurantManagement.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;

        public OrderService(OrderRepository orderRepository, OrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public Order CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            foreach (var orderItem in order.OrderItems)
            {
                _orderItemRepository.Add(orderItem);
            }
            return order;
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.Get(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public void UpdateOrder(int id, Order order)
        {
            var existingOrder = _orderRepository.Get(id);
            if (existingOrder == null)
                throw new Exception("Order not found.");

            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                throw new Exception("Order not found.");

            _orderRepository.Delete(id);
        }

        public void CancelOrder(int id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                throw new Exception("Order not found.");

            order.Status = OrderStatus.Annulée;
            _orderRepository.Update(order);
        }
    }
}

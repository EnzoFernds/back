using Microsoft.EntityFrameworkCore;
using RestaurantManagement;
using WebApplication1.Data;

namespace WebApplication1.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly MenuItemRepository _menuItemRepository;
        // ou OrderItemRepository si tu veux gérer ligne par ligne

        public OrderService(
            OrderRepository orderRepository,
            MenuItemRepository menuItemRepository,
            RestaurantContext context)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
            _context = context;

        }

        /// Crée une commande à partir d'un DTO et retourne l'entité persistée.
        public Order CreateOrder(CreateOrderDTO dto)
        {
            var order = new Order
            {
                ClientId = dto.UserId,
                RestaurantId = dto.RestaurantId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.EnCours,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;
            foreach (var i in dto.Items)
            {
                var menu = _menuItemRepository.Get(i.MenuItemId);
                if (menu == null)
                    throw new KeyNotFoundException($"MenuItem {i.MenuItemId} introuvable.");

                var sub = menu.Price * i.Quantity;
                total += sub;

                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = menu.MenuItemId,
                    Quantity = i.Quantity,
                    SubTotal = sub
                });
            }
            order.TotalAmount = total;

            _orderRepository.Add(order);
            _context.SaveChanges();   // si tu utilises directement le DbContext

            return order;
        }


        // --- Ajoutez ces méthodes ---

        public Order GetOrderById(int id)
        {
            return _orderRepository.Get(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public void UpdateOrder(int id, Order updatedOrder)
        {
            var existing = _orderRepository.Get(id);
            if (existing == null)
                throw new KeyNotFoundException($"Order {id} not found.");

            // Ici, on met à jour les champs modifiables
            existing.Status = updatedOrder.Status;
            existing.TotalAmount = updatedOrder.TotalAmount;
            // … si vous autorisez la modif des items, faites le mapping

            _orderRepository.Update(existing);
        }

        public void DeleteOrder(int id)
        {
            var existing = _orderRepository.Get(id);
            if (existing == null)
                throw new KeyNotFoundException($"Order {id} not found.");

            _orderRepository.Delete(id);
        }

        // Vos autres méthodes (CreateOrder, CancelOrder…)
    }
}

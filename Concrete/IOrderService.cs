using App.Data.Entities;

namespace SimoshStore;

public interface IOrderService
{
    public Task<IEnumerable<OrderItemEntity>> GetAllOrderItems();
    public Task<IEnumerable<OrderItemEntity>> GetOrderItemsByOrderIdAsync(int orderId);
    public Task<OrderEntity> GetOrderByIdAsync(int orderId);
    public Task<IEnumerable<OrderEntity>> GetAllOrdersAsync();
    public Task<OrderEntity> CreateOrderAsync(OrderEntity order);
    public Task<OrderEntity> UpdateOrderAsync(OrderEntity order);
    public Task DeleteOrderAsync(int orderId);
    public Task<IEnumerable<OrderEntity>> GetOrdersByUserIdAsync(int userId);

}

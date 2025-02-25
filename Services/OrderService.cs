using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimoshStore;

public class OrderService : IOrderService
{
    IDataRepository _Repository;
    public OrderService(IDataRepository repository)
    {
        _Repository = repository;
    }
    public async Task<IEnumerable<OrderItemEntity>> GetAllOrderItems()
    {
        var orderItems = await _Repository.GetAll<OrderItemEntity>().ToListAsync();
        if (orderItems == null)
        {
            return new List<OrderItemEntity>();
        }
        return orderItems;
    }
    public async Task<IEnumerable<OrderItemEntity>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        var orderItems = await _Repository.GetAll<OrderItemEntity>().Where(oi => oi.OrderId == orderId).ToListAsync();
        if (orderItems == null)
        {
            return new List<OrderItemEntity>();
        }
        return orderItems;
    }
    public async Task<OrderEntity> GetOrderByIdAsync(int orderId)
    {
        var order = await _Repository.GetByIdAsync<OrderEntity>(orderId);
        if (order == null)
        {
            return null;
        }
        return order;
    }
    public async Task<IEnumerable<OrderEntity>> GetAllOrdersAsync()
    {
        var orders = await _Repository.GetAll<OrderEntity>().ToListAsync();
        if (orders == null)
        {
            return new List<OrderEntity>();
        }
        return orders;
    }
    public async Task<OrderEntity> CreateOrderAsync(OrderEntity order)
    {
        await _Repository.AddAsync(order);
        return order;
    }
    public async Task<OrderEntity> UpdateOrderAsync(OrderEntity order)
    {
        await _Repository.UpdateAsync(order);
        return order;
    }
    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _Repository.GetByIdAsync<OrderEntity>(orderId);
        var orderItems = await GetOrderItemsByOrderIdAsync(orderId);
        foreach(var orderItem in orderItems)
        {
            await _Repository.DeleteAsync<OrderItemEntity>(orderItem.Id);
        }
        if (order != null)
        {
            await _Repository.DeleteAsync<OrderEntity>(orderId);
        }
    }
    public async Task<IEnumerable<OrderEntity>> GetOrdersByUserIdAsync(int userId)
    {
        var orders = await _Repository.GetAll<OrderEntity>().Where(o => o.UserId == userId).ToListAsync();
        if (orders == null)
        {
            return new List<OrderEntity>();
        }
        return orders;
    }

}

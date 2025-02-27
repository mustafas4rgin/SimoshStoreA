using App.Data.Entities;
using Microsoft.AspNetCore.Http.Features;
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
    public async Task<IEnumerable<CartItemEntity>> GetCartItemEntitiesByUserIdAsync(int userId)
    {
        var cartItems = await _Repository.GetAll<CartItemEntity>().Where(c=>c.UserId==userId).ToListAsync();
        return cartItems;
    }
    public async Task<IServiceResult> AddingOrderItemsAsync(OrderEntity order)
{
    // Sepetteki tüm öğeleri al
    var cartItems = await _Repository.GetAll<CartItemEntity>().Where(c => c.UserId == order.UserId).ToListAsync();

    // OrderItem'ları eklerken doğru fiyatları kullan
    foreach (var cartItem in cartItems)
    {
        var product = await _Repository.GetByIdAsync<ProductEntity>(cartItem.ProductId);
        
        decimal unitPrice = 0;
        
        // Eğer ürünün indirimi varsa, fiyatı indirimle hesapla
        if (product.DiscountId != 0)
        {
            var discount = await _Repository.GetByIdAsync<DiscountEntity>(product.DiscountId.Value);
            unitPrice = product.Price - (product.Price * discount.DiscountRate / 100); // İndirimli fiyat
        }
        else
        {
            unitPrice = product.Price; // İndirim yoksa, normal fiyat
        }

        // OrderItemEntity oluştur
        OrderItemEntity orderItemEntity = new OrderItemEntity
        {
            OrderId = order.Id,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            UnitPrice = unitPrice, // Her ürün için doğru fiyatı kullan
            CreatedAt = DateTime.UtcNow,
        };

        // OrderItem'ı veritabanına ekle
        await _Repository.AddAsync(orderItemEntity);
    }
    foreach(var item in cartItems)
    {
        await _Repository.DeleteAsync<CartItemEntity>(item.Id);
    }
    return new ServiceResult(true, "Added successfully");
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
    public async Task<OrderEntity> CreateOrderAsync(OrderDTO dto)
    {
        var order = MappingHelper.MappingOrderEntity(dto);
        await _Repository.AddAsync(order);
        var result = await AddingOrderItemsAsync(order);
        
        if(!result.Success)
        {
            return new OrderEntity();
        }
        return order;
    }
    public async Task<OrderEntity> UpdateOrderAsync(OrderDTO dto)
    {
        var order = MappingHelper.MappingOrderEntity(dto);
        await _Repository.UpdateAsync(order);
        return order;
    }
    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _Repository.GetByIdAsync<OrderEntity>(orderId);
        var orderItems = await GetOrderItemsByOrderIdAsync(orderId);
        foreach (var orderItem in orderItems)
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

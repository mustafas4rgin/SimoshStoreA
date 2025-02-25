using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                ViewBag.Error = "Order not found";
                return View();
            }
            await _orderService.DeleteOrderAsync(orderId);
            return RedirectToAction("ListOrders");
        }
        public async Task<IActionResult> OrderDetails(int? orderId)
        {
            // Check if orderId is null or invalid (e.g., non-positive number)
            if (!orderId.HasValue || orderId.Value <= 0)
            {
                // Redirect to NotFound page or handle it as per your business logic
                return RedirectToAction("NotFound", "Error");
            }

            var order = await _orderService.GetOrderByIdAsync(orderId.Value);

            if (order == null)
            {
                // If the order is not found, set an error message and redirect
                ViewBag.Error = "Order not found";
                return RedirectToAction("NotFound", "Error");
            }

            var orderItems = await _orderService.GetOrderItemsByOrderIdAsync(order.Id);
            var products = await _productService.ListAllProducts();
            ProductEntity product = null;

            // Assuming only one product should be associated with the order items
            foreach (var orderItem in orderItems)
            {
                product = products.FirstOrDefault(p => p.Id == orderItem.ProductId);
                if (product != null)
                {
                    break; // If we find the product, no need to keep iterating
                }
            }

            return View(new OrderDetailsViewModel
            {
                order = order,
                product = product,
                orderItems = orderItems.ToList()
            });
        }

        public async Task<IActionResult> ListOrders(int page = 1)
        {
            int pageSize = 10; // Maximum number of orders per page
            int userId = _userService.GetUserId();
            var user = await _userService.GetUserAsync(userId);
            if (!user.Success)
            {
                ViewBag.Error = user.Message;
                RedirectToAction("NotFound", "Error");
            }
            

            // Get all orders for the user
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            var orderItems = await _orderService.GetAllOrderItems();

            // Implement pagination: skip previous pages and take the current page's orders
            var pagedOrders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new OrderListViewModel
            {
                orders = pagedOrders,
                orderItems = orderItems.ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)orders.Count() / pageSize)
            };

            return View(model);
        }


    }
}

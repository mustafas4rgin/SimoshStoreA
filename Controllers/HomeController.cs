using System.Diagnostics;
using System.Security.Claims;
using App.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimoshStore.Models;
using System.Web;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimoshStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //DI Kesinlikle burada yapılmayacak sadece deneme için
    private readonly IDataRepository _Repository;
    private readonly IHttpContextAccessor _HttpContextAccessor;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBlogService _blogService;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;
    private readonly IProfileService _profileService;

    public HomeController(IUserService userService,IOrderService orderService,IProfileService profileService, IEmailService emailService, ILogger<HomeController> logger,IBlogService blogService, IDataRepository Repository, IHttpContextAccessor HttpContextAccessor, IProductService productService, ICategoryService categoryService)
    {
        _emailService = emailService;
        _logger = logger;
        _Repository = Repository;
        _HttpContextAccessor = HttpContextAccessor;
        _productService = productService;
        _categoryService = categoryService;
        _blogService = blogService;
        _userService = userService;
        _orderService = orderService;
        _profileService = profileService;
    }
    public async Task<IActionResult> Index()
    {
        var userIdClaim = _HttpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

        if (userIdClaim == null)
        {
            ViewData["CartCount"] = 0;
        }
        else
        {
            int userId = int.Parse(userIdClaim);
            var cartItems = _Repository.GetAll<CartItemEntity>().Where(x => x.UserId == userId).ToList();
            int cartCount = cartItems.Count();
            ViewData["CartCount"] = cartCount;
        }
        var discounts = await _Repository.GetAll<DiscountEntity>().ToListAsync();
        var Products = await _Repository.GetAll<ProductEntity>().ToListAsync();
        var productResult = await _productService.GetAllProductsAsync();
        var Categories = await _Repository.GetAll<CategoryEntity>().ToListAsync();
        var categoryResult = await _categoryService.GetAllCategoriesAsync();
        var Images = await _Repository.GetAll<ProductImageEntity>().ToListAsync();
        var popularProducts = await _productService.PopularProducts();
        var blogs = await _blogService.GetAllBlogsAsync();

        if (!productResult.Success || !categoryResult.Success)
        {
            string errorMessage = string.Empty;
            if (!productResult.Success)
                errorMessage += productResult.Message + " ";
            if (!categoryResult.Success)
                errorMessage += categoryResult.Message;

            ViewBag.ErrorMessage = errorMessage.Trim();
            return NotFound();
        }
        foreach (var product in Products)
        {
            product.Discount = discounts.FirstOrDefault(x => x.Id == product.DiscountId);
        }
        return View(
            new IndexViewModel
            {
                blogEntities = blogs.ToList(),
                popularProducts = popularProducts,
                products = Products,
                categories = Categories,
                images = Images,
                discounts = discounts,
                randomProducts = new List<ProductEntity>
                {
                    _productService.GetRandomProduct(),
                    _productService.GetRandomProduct()
                }
            }
        );
    }
    public async Task<IActionResult> AboutUs()
    {
        var comments =await _profileService.GetComments();
        foreach(var comment in comments)
        {
            comment.User = await _userService.GetUserByIdAsync(comment.UserId);
        }
        return View(comments);
    }
    public async Task<IActionResult> NewsLetter(NewsLetterViewModel model)
    {
        // await _emailService.SaveSubscriber(model.Email);
        await _emailService.SendEmailAsync("mustafas4rgin@gmail.com","NewsLetter",$"{model.Email} adresine sahip kişi haber bültenimize abone oldu.");
        await _emailService.SendEmailAsync(model.Email,"NewsLetter",$"Haber bültenimize abone olduğunuz için teşekkür ederiz. İndirim kodunuz: #{GenerateHelper.GenerateNumber()}");
        ViewData["Success"] = "Abone oldunuz.";
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> AboutMe()
    {
        var users = await _Repository.GetAll<UserEntity>().ToListAsync();
        var orders = await _Repository.GetAll<OrderEntity>().ToListAsync();
        var productComments = await _Repository.GetAll<ProductCommentEntity>().ToListAsync();
        return View(new AboutUsViewModel
        {
            users = users,
            orders = orders,
            productComments = productComments
        });
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [Authorize("Admin")]
    public IActionResult AdminDashboard()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult ContactUs()
    {
        return View(new ContactDTO());
    }
    [HttpPost]
    public async Task<IActionResult> ContactUs(ContactDTO dto)
    {
        if (ModelState.IsValid)
        {
                await _emailService.SendEmailAsync(
                to: "simoshstoreco@gmail.com",
                subject: "New Contact Us Message",
                body: $"Name: {dto.Name}\nEmail: {dto.Email}\nMessage: {dto.Message}"
            );
            var result = await _userService.ContactUsAsync(dto);
            if (result.Success)
            {
                ViewBag.Message = "Your message has been sent successfully.";
            }
            else
            {
                ViewBag.Message = "An error occurred while sending your message.";
            }
        }
        else
        {
            ViewBag.Message = "Please fill in all fields correctly.";
        }

        return RedirectToAction("Index","Home");
    }   
}

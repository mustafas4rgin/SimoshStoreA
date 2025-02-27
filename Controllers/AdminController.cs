using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SimoshStore.Controllers
{
    [Authorize(Roles = "admin")] // Bu controller'a yalnızca Admin rolüyle erişilebilir.
    public class AdminController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public AdminController(IDataRepository repository, IProductService productService, ICategoryService categoryService)
        {
            _repository = repository;
            _productService = productService;
            _categoryService = categoryService;
        }
        // AdminDashboard action'ı, Admin rolüne sahip kullanıcılar tarafından erişilebilir.
        public IActionResult AdminDashboard()
        {
            // Admin dashboard içeriğini burada render edebilirsiniz.
            // Örneğin, Admin'e özel veri, raporlar, kullanıcı yönetimi vb. gösterebilirsiniz.
            return View();
        }

        // Admin kullanıcıları yönetebileceği bir action örneği
        public IActionResult ManageUsers()
        {
            // Kullanıcı yönetimi işlemleri yapılabilir
            return View();
        }

        // Admin raporları görüntüleyebileceği bir action örneği
        public IActionResult ViewReports()
        {
            // Raporları görüntülemek için gerekli işlemler yapılabilir
            return View();
        }
        public async Task<IActionResult> ProductList()
        {
            var products = _repository.GetAll<ProductEntity>().ToList();
            var result = await _productService.GetAllProductsAsync();
            if(!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            return View("ProductList",products);
        }
        public async Task<IActionResult> CategoryList()
        {
            var categories = _repository.GetAll<CategoryEntity>().ToList();
            var result = await _categoryService.GetAllCategoriesAsync();
            if(!result.Success)
            {
                ViewBag.Error = result.Message;
                return View("CategoryList",categories);
            }
            return View(categories);
        }
        public async Task<IActionResult> ListBlogCategories()
        {
            var blogCategories =await _repository.GetAll<BlogCategoryEntity>().ToListAsync();
            return View(blogCategories);
        }
        public async Task<IActionResult> ListBlogs()
        {
            var blogs = await _repository.GetAll<BlogEntity>().ToListAsync();
            return View(blogs);
        }
    }
}

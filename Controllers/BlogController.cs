using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoshStore;

namespace MyApp.Namespace
{
    public class BlogController : Controller
    {
        ITagService _tagService;
        IDataRepository _Repository;
        IBlogService _blogService;
        IBlogCategoryService _blogCategoryService;
        ICategoryService _categoryService;
        IProductService _productService;
        public BlogController(ITagService tagService, ICategoryService categoryService, IProductService productService, IDataRepository dataRepository, IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _Repository = dataRepository;
            _blogCategoryService = blogCategoryService;
            _blogService = blogService;
            _productService = productService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
        public async Task<IActionResult> BlogPost(int id)
        {
            var randomBlog = await _blogService.GetRandomBlog();
            var blogEntity = await _blogService.GetBlogByIdAsync(id);
            var blogEntities = await _blogService.GetAllBlogsAsync();
            var blogComments = await _blogService.GetComments(id);
            var relBlogTags = await _Repository.GetAll<RelBlogTagEntity>().ToListAsync();
            var user = await _Repository.GetByIdAsync<UserEntity>(blogEntity.UserId);
            List<BlogTagEntity> tags = new List<BlogTagEntity>();
            foreach (var relBlogTag in relBlogTags)
            {
                var tag = await _tagService.GetTagByIdAsync(relBlogTag.TagId);
                tags.Add(tag);
            }
            if (user is null)
            {
                user = new UserEntity();
            }
            return View(new BlogPostViewModel
            {
                randomBlog = randomBlog,
                blog = blogEntity,
                blogs = blogEntities,
                blogComments = blogComments,
                user = user,
                quote = QuoteHelper.GenerateQuote(),
                tags = tags

            });

        }
        public async Task<IActionResult> BlogList()
        {
            var blogEntities = await _blogService.GetAllBlogsAsync();
            var blogCategoryEntities = await _blogCategoryService.GetAllCategoriesAsync();
            return View(new BlogListViewModel
            {
                blogCategoryEntities = blogCategoryEntities,
                blogEntities = blogEntities,

            });
        }
        public async Task<IActionResult> BlogListWithTag(BlogTagEntity blogTagEntity)
        {
            return View();
        }

    }
}

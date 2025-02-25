using System.Threading.Tasks;
using App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using SimoshStore;

namespace MyApp.Namespace
{
    public class CommentController : Controller
    {
        private readonly IDataRepository _Repository;
        public CommentController(IDataRepository repository)
        {
            _Repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, int userId, string email, string commentText, int rating)
        {
            var user = _Repository.GetAll<UserEntity>().Where(x => x.Email == email).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var comment = new ProductCommentEntity
                {
                    ProductId = 1,
                    UserId = 1,
                    StarCount = rating,
                    Text = commentText
                };
                await _Repository.AddAsync(comment);

                return RedirectToAction("ProductDetails", "Shop", new { id = 1 });
            }
            return RedirectToAction("ProductDetails", "Shop", new { id = 1 });
        }
    }
}

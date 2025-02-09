using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimoshStore.Controllers
{
    [Authorize(Roles = "Admin")] // Bu controller'a yalnızca Admin rolüyle erişilebilir.
    public class AdminController : Controller
    {
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
    }
}

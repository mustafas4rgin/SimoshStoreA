using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimoshStore.Models;

namespace SimoshStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //DI Kesinlikle burada yapılmayacak sadece deneme için
    private readonly AppDbContext _appDbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        var userRoles = _appDbContext.UserRoles.ToList();
        return View(userRoles);
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
}

using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Learner");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

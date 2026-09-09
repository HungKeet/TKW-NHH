using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tuan4.Models;

namespace Tuan4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var newProducts = Product.GetNewProducts();
            return View(newProducts);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Trang giới thiệu thông tin ứng dụng ASP.NET Core MVC.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Thông tin liên hệ & Hỗ trợ học tập Devmaster Academy.";
            return View();
        }

        public IActionResult DemoView()
        {
            return View();
        }

        public IActionResult ProductDemo()
        {
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

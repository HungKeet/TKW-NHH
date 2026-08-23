using Microsoft.AspNetCore.Mvc;

namespace NHH.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
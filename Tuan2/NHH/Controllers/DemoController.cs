using Microsoft.AspNetCore.Mvc;
namespace NHH.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

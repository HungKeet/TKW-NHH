using Microsoft.AspNetCore.Mvc;
using Tuan4.Models;

namespace Tuan4.ViewComponents
{
    public class HotProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var hotProducts = Product.GetHotProducts();
            return View(hotProducts);
        }
    }
}

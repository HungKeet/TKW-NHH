using Microsoft.AspNetCore.Mvc;

namespace Tuan9.Areas.Admin.Components;

public class NavLeftViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var currentController = ViewContext.RouteData.Values["controller"]?.ToString() ?? "";
        var currentAction = ViewContext.RouteData.Values["action"]?.ToString() ?? "";
        ViewBag.CurrentController = currentController;
        ViewBag.CurrentAction = currentAction;
        return View();
    }
}

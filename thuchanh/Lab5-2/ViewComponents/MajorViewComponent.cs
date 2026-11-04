using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.ViewComponents;

public class MajorViewComponent : ViewComponent
{
    private readonly SchoolContext db;

    public MajorViewComponent(SchoolContext context)
    {
        db = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var majors = db.Majors.ToList();
        return View("RenderMajor", majors);
    }
}

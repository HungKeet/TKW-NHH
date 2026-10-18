using System.Collections.Generic;

namespace Tuan9.Models;

public class HomeViewModel
{
    public List<Banner> Banners { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<Blog> Blogs { get; set; } = new();
}

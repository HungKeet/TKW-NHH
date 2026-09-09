using System.Collections.Generic;

namespace Tuan4.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;

        public static List<string> GetCategories()
        {
            return new List<string>
            {
                "Áo dài",
                "Áo đông",
                "Túi xách",
                "Đồng hồ",
                "Ví da",
                "Thắt lưng da",
                "Tủ lạnh",
                "Tivi",
                "Quạt điện",
                "Lò sưởi"
            };
        }

        public static List<Product> GetNewProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/nagakawa.jpg", Price = 1850000, Category = "Đồ gia dụng" },
                new Product { Id = 2, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/nagakawa.jpg", Price = 1850000, Category = "Đồ gia dụng" },
                new Product { Id = 3, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/nagakawa.jpg", Price = 1850000, Category = "Đồ gia dụng" },
            };
        }

        public static List<Product> GetHotProducts()
        {
            return new List<Product>
            {
                new Product { Id = 4, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/nagakawa.jpg", Price = 1850000, Category = "Đồ gia dụng" },
                new Product { Id = 5, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/nagakawa.jpg", Price = 1850000, Category = "Đồ gia dụng" },
                new Product { Id = 6, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Image = "/images/products/nagakawa.jpg", Price = 1850000, Category = "Đồ gia dụng" },
            };
        }
    }
}

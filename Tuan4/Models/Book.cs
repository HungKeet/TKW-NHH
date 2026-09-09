using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tuan4.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; } = string.Empty;
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Sumary { get; set; } = string.Empty;

        // Danh sách các cuốn sách
        public List<Book> GetBookList()
        {
            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Chí Phèo",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b1.jpg",
                    Price = 500000,
                    Sumary = "Tác phẩm kinh điển của nhà văn Nam Cao viết về số phận người nông dân nghèo trước Cách mạng tháng Tám.",
                    TotalPage = 250
                },
                new Book()
                {
                    Id = 2,
                    Title = "Lão Hạc",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b2.jpg",
                    Price = 700000,
                    Sumary = "Truyện ngắn xuất sắc khắc họa bi kịch và nhân cách cao đẹp của người nông dân nghèo.",
                    TotalPage = 200
                },
                new Book()
                {
                    Id = 4,
                    Title = "Conan Phiêu lưu ký",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b3.jpg",
                    Price = 550000,
                    Sumary = "Bộ truyện tranh / tiểu thuyết phiêu lưu nổi tiếng thế giới.",
                    TotalPage = 180
                },
                new Book()
                {
                    Id = 6,
                    Title = "Đường Xưa Mây Trắng",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "/images/products/b4.jpg",
                    Price = 850000,
                    Sumary = "Tác phẩm kể về cuộc đời Đức Phật qua ngòi bút của Thiền sư Thích Nhất Hạnh.",
                    TotalPage = 350
                },
            };
            return books;
        }

        // Chi tiết một cuốn sách theo id
        public Book? GetBookById(int id)
        {
            Book? book = this.GetBookList().FirstOrDefault(b => b.Id == id);
            return book;
        }

        // Thuộc tính Authors trả về SelectListItem Authors
        public List<SelectListItem> Authors { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Nam cao" },
            new SelectListItem { Value = "2", Text = "Ngô Tất Tố" },
            new SelectListItem { Value = "3", Text = "Adamkhoom" },
            new SelectListItem { Value = "4", Text = "Thiền sư Thích Nhất Hạnh" }
        };

        // Thuộc tính Genres trả về SelectListItem Genres
        public List<SelectListItem> Genres { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Truyện tranh" },
            new SelectListItem { Value = "2", Text = "Văn học đương đại" },
            new SelectListItem { Value = "3", Text = "Phật học phổ thông" },
            new SelectListItem { Value = "4", Text = "Truyền cười" }
        };
    }
}

namespace NetCoreMVCLab03.Models
{
    // Alias namespace
    public class Book : Tuan4.Models.Book { }
}

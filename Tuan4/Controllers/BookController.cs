using Microsoft.AspNetCore.Mvc;
using Tuan4.Models;
using System.Linq;

namespace Tuan4.Controllers
{
    public class BookController : Controller
    {
        protected Book book = new Book();

        // GET: /Book/Index hoặc lọc dữ liệu
        [HttpGet]
        public IActionResult Index()
        {
            // Danh sách authors & genres để hiển thị trên combobox
            ViewBag.authors = book.Authors; // truyền dữ liệu SelectListItem qua view
            ViewBag.genres = book.Genres;   // truyền dữ liệu SelectListItem qua view
            var books = book.GetBookList();
            return View(books); // truyền dữ liệu qua view dưới dạng tham số
        }

        // POST: Tìm kiếm theo ComboBox
        [HttpPost]
        public IActionResult Index(int? authorId, int? genreId)
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;
            ViewBag.selectedAuthor = authorId;
            ViewBag.selectedGenre = genreId;

            var books = book.GetBookList();
            if (authorId.HasValue && authorId.Value > 0)
            {
                books = books.Where(b => b.AuthorId == authorId.Value).ToList();
            }
            if (genreId.HasValue && genreId.Value > 0)
            {
                books = books.Where(b => b.GenreId == genreId.Value).ToList();
            }
            return View(books);
        }

        // GET: /Book/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.authors = book.Authors; // truyền dữ liệu SelectListItem qua view
            ViewBag.genres = book.Genres;   // truyền dữ liệu SelectListItem qua view
            Book model = new Book();
            return View(model);
        }

        // POST: /Book/Create
        [HttpPost]
        public IActionResult Create(Book model)
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: /Book/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.authors = book.Authors; // truyền dữ liệu SelectListItem qua view
            ViewBag.genres = book.Genres;   // truyền dữ liệu SelectListItem qua view
            Book? model = book.GetBookById(id); // lấy dữ liệu một cuốn sách theo id
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: /Book/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Book model)
        {
            return RedirectToAction(nameof(Index));
        }

        // Gọi PartialView thông qua AJAX
        public PartialViewResult PopularBook()
        {
            var books = book.GetBookList();
            return PartialView(books);
        }
    }
}

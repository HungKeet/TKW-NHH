using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tuan8.Models.BusinessModels;
using Tuan8.Models.DataModels;

namespace Tuan8.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManagementContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(BookManagementContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Book
        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            var query = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;

            var books = await query.ToListAsync();
            return View(books);
        }

        // GET: Book/Details/B001
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book, IFormFile fileImage)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng khóa chính
                if (await _context.Books.AnyAsync(b => b.BookId == book.BookId))
                {
                    ModelState.AddModelError("BookId", "Mã sách này đã tồn tại trong hệ thống.");
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
                    ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
                    return View(book);
                }

                // Xử lý upload ảnh nếu có
                if (fileImage != null && fileImage.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileImage.CopyToAsync(stream);
                    }
                    book.Picture = fileName;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // GET: Book/Edit/B001
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // POST: Book/Edit/B001
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book, IFormFile fileImage)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy bản ghi hiện tại từ DB để không bị mất ảnh cũ nếu không chọn ảnh mới
                    var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);

                    if (fileImage != null && fileImage.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);
                        var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await fileImage.CopyToAsync(stream);
                        }
                        book.Picture = fileName;
                    }
                    else if (existingBook != null)
                    {
                        book.Picture = existingBook.Picture;
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // GET: Book/Delete/B001
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/B001
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}

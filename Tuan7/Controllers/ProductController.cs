using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tuan7.Models;

namespace Tuan7.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" },
            new Category { Id = 3, Name = "Phụ kiện" },
            new Category { Id = 4, Name = "Đồng hồ thông minh" }
        };

        public static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "iPhone 15 Pro Max",
                Image = "iphone15.png",
                Price = 30000000,
                SalePrice = 26000000,
                CategoryId = 1,
                Description = "Điện thoại flagship cao cấp mới nhất từ Apple với chip A17 Pro và khung vỏ titan siêu nhẹ."
            },
            new Product
            {
                Id = 2,
                Name = "MacBook Pro 14 M3",
                Image = "macbook.png",
                Price = 45000000,
                SalePrice = 39000000,
                CategoryId = 2,
                Description = "Máy tính xách tay cấu hình cao hỗ trợ lập trình viên phát triển ứng dụng chuyên sâu."
            }
        };

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product
        public IActionResult Index()
        {
            ViewBag.Categories = categories.ToDictionary(c => c.Id, c => c.Name);
            return View(products);
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryName = categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(new Product { Price = 100000, SalePrice = 80000 });
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model, IFormFile ImageFile)
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError("Image", "Hình ảnh sản phẩm bắt buộc phải chọn và upload");
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }
                    model.Image = uniqueFileName;
                }

                model.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
                products.Add(model);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model, IFormFile ImageFile)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }
                    existingProduct.Image = uniqueFileName;
                }

                existingProduct.Name = model.Name;
                existingProduct.Price = model.Price;
                existingProduct.SalePrice = model.SalePrice;
                existingProduct.CategoryId = model.CategoryId;
                existingProduct.Description = model.Description;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryName = categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Tuan4.Models;

namespace Tuan4.ViewComponents
{
    // ViewComponent Book
    public class BookViewComponent : ViewComponent
    {
        protected Book book = new Book();

        public IViewComponentResult Invoke()
        {
            var books = book.GetBookList();
            return View(books);
        }
    }
}

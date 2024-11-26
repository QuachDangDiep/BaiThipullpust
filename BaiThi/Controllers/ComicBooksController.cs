using baithi.Data;
using baithi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace baithi.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicSystemMemoryDbContext _context;

        public ComicBooksController(ComicSystemMemoryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ComicBooks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ComicBook comicBook)
        {
            comicBook.ComicBookId = _context.ComicBooks.Count + 1;
            _context.ComicBooks.Add(comicBook);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var comicBook = _context.ComicBooks.FirstOrDefault(cb => cb.ComicBookId == id);
            return View(comicBook);
        }

        [HttpPost]
        public IActionResult Edit(ComicBook comicBook)
        {
            var existing = _context.ComicBooks.FirstOrDefault(cb => cb.ComicBookId == comicBook.ComicBookId);
            if (existing != null)
            {
                existing.Title = comicBook.Title;
                existing.Author = comicBook.Author;
                existing.PricePerDay = comicBook.PricePerDay;
                existing.StockQuantity = comicBook.StockQuantity;
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var comicBook = _context.ComicBooks.FirstOrDefault(cb => cb.ComicBookId == id);
            return View(comicBook);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var comicBook = _context.ComicBooks.FirstOrDefault(cb => cb.ComicBookId == id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

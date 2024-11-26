using baithi.Data;
using baithi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace baithi.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemMemoryDbContext _context;

        public RentalsController(ComicSystemMemoryDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách các giao dịch cho thuê sách
        public IActionResult Index()
        {
            return View(_context.Rentals);
        }

        // Trang cho thuê sách
        public IActionResult RentBook()
        {
            ViewBag.Customers = _context.Customers;
            ViewBag.ComicBooks = _context.ComicBooks;
            return View();
        }

        [HttpPost]
        public IActionResult RentBook(int customerId, int comicBookId, int quantity, DateTime returnDate)
        {
            var comicBook = _context.ComicBooks.FirstOrDefault(cb => cb.ComicBookId == comicBookId);

            if (comicBook != null && comicBook.StockQuantity >= quantity)
            {
                // Tạo giao dịch cho thuê mới
                var rental = new Rental
                {
                    RentalId = _context.Rentals.Count + 1,
                    CustomerId = customerId,
                    RentalDate = DateTime.Now,
                    ReturnDate = returnDate,
                };

                // Thêm chi tiết giao dịch
                var rentalDetail = new RentalDetail
                {
                    RentalDetailId = _context.Rentals.SelectMany(r => r.RentalDetails).Count() + 1,
                    RentalId = rental.RentalId,
                    ComicBookId = comicBookId,
                    Quantity = quantity,
                    PricePerDay = comicBook.PricePerDay
                };

                rental.RentalDetails.Add(rentalDetail);

                // Cập nhật tồn kho
                comicBook.StockQuantity -= quantity;

                // Lưu giao dịch
                _context.Rentals.Add(rental);

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Not enough stock available.");
            ViewBag.Customers = _context.Customers;
            ViewBag.ComicBooks = _context.ComicBooks;
            return View();
        }

        // Hiển thị báo cáo giao dịch
        public IActionResult Report(DateTime startDate, DateTime endDate)
        {
            var report = _context.Rentals
                .Where(r => r.RentalDate >= startDate && r.RentalDate <= endDate)
                .SelectMany(r => r.RentalDetails)
                .Join(
                    _context.ComicBooks,
                    rd => rd.ComicBookId,
                    cb => cb.ComicBookId,
                    (rd, cb) => new
                    {
                        ComicBookTitle = cb.Title,
                        RentalDate = r.RentalDate,
                        Quantity = rd.Quantity,
                        TotalPrice = rd.Quantity * rd.PricePerDay * (r.ReturnDate - r.RentalDate).Days
                    })
                .ToList();

            return View(report);
        }
    }
}

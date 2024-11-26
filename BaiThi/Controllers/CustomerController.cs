using baithi.Data;
using baithi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace baithi.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicSystemMemoryDbContext _context;

        public CustomersController(ComicSystemMemoryDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách khách hàng
        public IActionResult Index()
        {
            return View(_context.Customers);
        }

        // Hiển thị trang đăng ký khách hàng mới
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerId = _context.Customers.Count + 1;
                customer.RegisterDate = DateTime.Now;
                _context.Customers.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}

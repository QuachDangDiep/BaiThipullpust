using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemDbContext _context;

        public RentalsController(ComicSystemDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Customer) // Include the Customer data
                .Include(r => r.RentalDetails) // Include the related RentalDetails
                    .ThenInclude(rd => rd.ComicBook) // Then Include the related ComicBook for each RentalDetail
                .ToListAsync();

            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList(); // Populate customer list
            ViewBag.ComicBooks = _context.ComicBooks.ToList(); // Populate comic book list
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental, List<RentalDetail> rentalDetails)
        {
            if (ModelState.IsValid)
            {
                // Add rental entry
                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                // Add rental details (comic books rented)
                foreach (var detail in rentalDetails)
                {
                    detail.RentalID = rental.RentalID; // Link rental details with rental
                    _context.RentalDetails.Add(detail);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If model state is invalid, return to the form with error messages
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.ComicBooks = _context.ComicBooks.ToList();
            return View(rental);
        }
    }
}

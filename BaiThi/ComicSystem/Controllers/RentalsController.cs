using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class RentalsController : Controller
{
    private readonly ComicSystemContext _context;

    public RentalsController(ComicSystemContext context)
    {
        _context = context;
    }

    // GET: Rentals/RentBook
    public IActionResult RentBook()
    {
        ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "FullName");
        ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RentBook(int customerId, int comicBookId, DateTime rentalDate, DateTime returnDate, int quantity)
    {
        var rental = new Rental
        {
            CustomerID = customerId,
            RentalDate = rentalDate,
            ReturnDate = returnDate,
            Status = "Đang thuê"
        };
        _context.Add(rental);
        await _context.SaveChangesAsync();

        var rentalDetail = new RentalDetail
        {
            RentalID = rental.RentalID,
            ComicBookID = comicBookId,
            Quantity = quantity,
            PricePerDay = _context.ComicBooks.Find(comicBookId).PricePerDay
        };
        _context.Add(rentalDetail);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}

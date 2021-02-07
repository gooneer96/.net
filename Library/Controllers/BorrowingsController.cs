using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    public class BorrowingsController : Controller
    {
        private readonly LibraryContext _context;        

        public BorrowingsController(LibraryContext context)
        {
            _context = context;
            
        }

        // GET: Borrowings
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "state_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var libraryContext = _context.Borrowings.Include(b => b.Book).Include(b => b.Borrower);

            switch (sortOrder)
            {
                case "state_desc":
                    libraryContext = libraryContext.OrderByDescending(b => b.State).Include(b => b.Book).Include(b => b.Borrower);
                    break;
                case "Date":
                    libraryContext = libraryContext.OrderBy(s => s.RentalDate).Include(b => b.Book).Include(b => b.Borrower);
                    break;
                case "date_desc":
                    libraryContext = libraryContext.OrderByDescending(s => s.RentalDate).Include(b => b.Book).Include(b => b.Borrower);
                    break;
                default:
                    libraryContext = libraryContext.OrderBy(s => s.State).Include(b => b.Book).Include(b => b.Borrower);
                    break;
            }

            
            return View(await libraryContext.ToListAsync());
        }

        // GET: Borrowings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: Borrowings/Create
        public IActionResult Create()
        {
                     

            ViewData["BookID"] = new SelectList(_context.Books, "ID", "Title");
            ViewData["BorrowerID"] = new SelectList(_context.Users, "ID", "FirstName");

            
            return View();
        }

        // POST: Borrowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RentalDate,EndDate,State,BookID,BorrowerID")] Borrowing borrowing)
        {
          
          

            if (ModelState.IsValid)
            {

                var obj = _context.Borrowings.Where(b => b.BookID.Equals(borrowing.BookID)).Where(b=> b.EndDate == null).ToList();

                if (obj.Count==0)
                {
                   _context.Add(borrowing);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Book is already borrowed";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "Title", borrowing.BookID);
            ViewData["BorrowerID"] = new SelectList(_context.Users, "ID", "FirstName", borrowing.BorrowerID);


          

            return View(borrowing);
        }

        
        public async Task<IActionResult> EndBorrow(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            borrowing.EndBorrowing();
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
/*
        // POST: Borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RentalDate,EndDate,State,BookID,BorrowerID")] Borrowing borrowing)
        {
            if (id != borrowing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowingExists(borrowing.ID))
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
            ViewData["BookID"] = new SelectList(_context.Books, "ID", "Title", borrowing.BookID);
            ViewData["BorrowerID"] = new SelectList(_context.Users, "ID", "ID", borrowing.BorrowerID);
            return View(borrowing);
        }*/

        // GET: Borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Borrower)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowing = await _context.Borrowings.FindAsync(id);
            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowings.Any(e => e.ID == id);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyEmployeeCrudApp.Data;
using CompanyEmployeeCrudApp.Models;

namespace CompanyEmployeeCrudApp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Companies
        public async Task<IActionResult> Index()
        {
            var companies = await _context.Companies.ToListAsync();
            return View(companies);
        }

        // GET: /Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        // GET: /Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Logo,Website")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: /Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound();

            return View(company);
        }

        // POST: /Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Logo,Website")] Company company)
        {
            if (id != company.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: /Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        // POST: /Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company!);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}

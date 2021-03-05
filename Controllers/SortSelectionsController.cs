using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class SortSelectionsController : Controller
    {
        private readonly CinemaOnlineContext _context;

        public SortSelectionsController(CinemaOnlineContext context)
        {
            _context = context;
        }

        // GET: SortSelections
        public async Task<IActionResult> Index()
        {
            var cinemaOnlineContext = _context.SortSelections.Include(s => s.Film).Include(s => s.Selection);
            return View(await cinemaOnlineContext.ToListAsync());
        }

        // GET: SortSelections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortSelection = await _context.SortSelections
                .Include(s => s.Film)
                .Include(s => s.Selection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortSelection == null)
            {
                return NotFound();
            }

            return View(sortSelection);
        }

        // GET: SortSelections/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name");
            ViewData["SelectionId"] = new SelectList(_context.Selections, "Id", "Name");
            return View();
        }

        // POST: SortSelections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SortingInfo,Genre,SelectionId,FilmId")] SortSelection sortSelection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortSelection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", sortSelection.FilmId);
            ViewData["SelectionId"] = new SelectList(_context.Selections, "Id", "Name", sortSelection.SelectionId);
            return View(sortSelection);
        }

        // GET: SortSelections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortSelection = await _context.SortSelections.FindAsync(id);
            if (sortSelection == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", sortSelection.FilmId);
            ViewData["SelectionId"] = new SelectList(_context.Selections, "Id", "Name", sortSelection.SelectionId);
            return View(sortSelection);
        }

        // POST: SortSelections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SortingInfo,Genre,SelectionId,FilmId")] SortSelection sortSelection)
        {
            if (id != sortSelection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortSelection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortSelectionExists(sortSelection.Id))
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
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", sortSelection.FilmId);
            ViewData["SelectionId"] = new SelectList(_context.Selections, "Id", "Name", sortSelection.SelectionId);
            return View(sortSelection);
        }

        // GET: SortSelections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sortSelection = await _context.SortSelections
                .Include(s => s.Film)
                .Include(s => s.Selection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortSelection == null)
            {
                return NotFound();
            }

            return View(sortSelection);
        }

        // POST: SortSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sortSelection = await _context.SortSelections.FindAsync(id);
            _context.SortSelections.Remove(sortSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortSelectionExists(int id)
        {
            return _context.SortSelections.Any(e => e.Id == id);
        }
    }
}

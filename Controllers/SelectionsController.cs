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
    public class SelectionsController : Controller
    {
        private readonly CinemaOnlineContext _context;

        public SelectionsController(CinemaOnlineContext context)
        {
            _context = context;
        }

        // GET: Selections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Selections.ToListAsync());
        }

        // GET: Selections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selection = await _context.Selections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selection == null)
            {
                return NotFound();
            }

            return View(selection);
        }

        // GET: Selections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Selections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,QuantityFilm,Info")] Selection selection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(selection);
        }

        // GET: Selections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selection = await _context.Selections.FindAsync(id);
            if (selection == null)
            {
                return NotFound();
            }
            return View(selection);
        }

        // POST: Selections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,QuantityFilm,Info")] Selection selection)
        {
            if (id != selection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectionExists(selection.Id))
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
            return View(selection);
        }

        // GET: Selections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selection = await _context.Selections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selection == null)
            {
                return NotFound();
            }

            return View(selection);
        }

        // POST: Selections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selection = await _context.Selections.FindAsync(id);
            _context.Selections.Remove(selection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelectionExists(int id)
        {
            return _context.Selections.Any(e => e.Id == id);
        }
    }
}

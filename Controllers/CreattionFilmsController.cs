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
    public class CreattionFilmsController : Controller
    {
        private readonly CinemaOnlineContext _context;

        public CreattionFilmsController(CinemaOnlineContext context)
        {
            _context = context;
        }

        // GET: CreattionFilms
        public async Task<IActionResult> Index()
        {
            var cinemaOnlineContext = _context.CreattionFilms.Include(c => c.Creator).Include(c => c.Film);
            return View(await cinemaOnlineContext.ToListAsync());
        }

        // GET: CreattionFilms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creattionFilm = await _context.CreattionFilms
                .Include(c => c.Creator)
                .Include(c => c.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creattionFilm == null)
            {
                return NotFound();
            }

            return View(creattionFilm);
        }

        // GET: CreattionFilms/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Creators, "Id", "Name");
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name");
            return View();
        }

        // POST: CreattionFilms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Profession,Salary,FilmId,CreatorId")] CreattionFilm creattionFilm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creattionFilm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Creators, "Id", "Name", creattionFilm.CreatorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", creattionFilm.FilmId);
            return View(creattionFilm);
        }

        // GET: CreattionFilms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creattionFilm = await _context.CreattionFilms.FindAsync(id);
            if (creattionFilm == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Creators, "Id", "Name", creattionFilm.CreatorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", creattionFilm.FilmId);
            return View(creattionFilm);
        }

        // POST: CreattionFilms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Profession,Salary,FilmId,CreatorId")] CreattionFilm creattionFilm)
        {
            if (id != creattionFilm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creattionFilm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreattionFilmExists(creattionFilm.Id))
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
            ViewData["CreatorId"] = new SelectList(_context.Creators, "Id", "Name", creattionFilm.CreatorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", creattionFilm.FilmId);
            return View(creattionFilm);
        }

        // GET: CreattionFilms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creattionFilm = await _context.CreattionFilms
                .Include(c => c.Creator)
                .Include(c => c.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creattionFilm == null)
            {
                return NotFound();
            }

            return View(creattionFilm);
        }

        // POST: CreattionFilms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creattionFilm = await _context.CreattionFilms.FindAsync(id);
            _context.CreattionFilms.Remove(creattionFilm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreattionFilmExists(int id)
        {
            return _context.CreattionFilms.Any(e => e.Id == id);
        }
    }
}

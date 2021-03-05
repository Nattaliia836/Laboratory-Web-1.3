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
    public class ActorPlaysController : Controller
    {
        private readonly CinemaOnlineContext _context;

        public ActorPlaysController(CinemaOnlineContext context)
        {
            _context = context;
        }

        // GET: ActorPlays
        public async Task<IActionResult> Index()
        {
            var cinemaOnlineContext = _context.ActorPlays.Include(a => a.Actor).Include(a => a.Film);
            return View(await cinemaOnlineContext.ToListAsync());
        }

        // GET: ActorPlays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorPlay = await _context.ActorPlays
                .Include(a => a.Actor)
                .Include(a => a.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorPlay == null)
            {
                return NotFound();
            }

            return View(actorPlay);
        }

        // GET: ActorPlays/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Name");
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name");
            return View();
        }

        // POST: ActorPlays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Role,Salary,QuantityScenes,FilmId,ActorId")] ActorPlay actorPlay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorPlay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Name", actorPlay.ActorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", actorPlay.FilmId);
            return View(actorPlay);
        }

        // GET: ActorPlays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorPlay = await _context.ActorPlays.FindAsync(id);
            if (actorPlay == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Name", actorPlay.ActorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", actorPlay.FilmId);
            return View(actorPlay);
        }

        // POST: ActorPlays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Role,Salary,QuantityScenes,FilmId,ActorId")] ActorPlay actorPlay)
        {
            if (id != actorPlay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorPlay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorPlayExists(actorPlay.Id))
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
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Name", actorPlay.ActorId);
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Name", actorPlay.FilmId);
            return View(actorPlay);
        }

        // GET: ActorPlays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorPlay = await _context.ActorPlays
                .Include(a => a.Actor)
                .Include(a => a.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorPlay == null)
            {
                return NotFound();
            }

            return View(actorPlay);
        }

        // POST: ActorPlays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorPlay = await _context.ActorPlays.FindAsync(id);
            _context.ActorPlays.Remove(actorPlay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorPlayExists(int id)
        {
            return _context.ActorPlays.Any(e => e.Id == id);
        }
    }
}

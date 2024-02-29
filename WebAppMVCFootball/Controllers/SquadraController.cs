using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMVCFootball.Models;

namespace WebAppMVCFootball.Controllers
{
    public class SquadraController : Controller
    {
        private readonly FootballContext _context;

        public SquadraController(FootballContext context)
        {
            _context = context;
        }

        // GET: Squadra
        public async Task<IActionResult> Index()
        {
            var footballContext = _context.TSquadras.Include(t => t.IdCampionatoNavigation);
            return View(await footballContext.ToListAsync());
        }

        // GET: Squadra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSquadra = await _context.TSquadras
                .Include(t => t.IdCampionatoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tSquadra == null)
            {
                return NotFound();
            }

            return View(tSquadra);
        }

        // GET: Squadra/Create
        public IActionResult Create()
        {
            ViewData["IdCampionato"] = new SelectList(_context.TCampionatos, "Id", "Id");
            return View();
        }

        // POST: Squadra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCampionato,Nome,AnnoFondazione")] TSquadra tSquadra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSquadra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCampionato"] = new SelectList(_context.TCampionatos, "Id", "Id", tSquadra.IdCampionato);
            return View(tSquadra);
        }

        // GET: Squadra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSquadra = await _context.TSquadras.FindAsync(id);
            if (tSquadra == null)
            {
                return NotFound();
            }
            ViewData["IdCampionato"] = new SelectList(_context.TCampionatos, "Id", "Id", tSquadra.IdCampionato);
            return View(tSquadra);
        }

        // POST: Squadra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCampionato,Nome,AnnoFondazione")] TSquadra tSquadra)
        {
            if (id != tSquadra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSquadra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSquadraExists(tSquadra.Id))
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
            ViewData["IdCampionato"] = new SelectList(_context.TCampionatos, "Id", "Id", tSquadra.IdCampionato);
            return View(tSquadra);
        }

        // GET: Squadra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSquadra = await _context.TSquadras
                .Include(t => t.IdCampionatoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tSquadra == null)
            {
                return NotFound();
            }

            return View(tSquadra);
        }

        // POST: Squadra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tSquadra = await _context.TSquadras.FindAsync(id);
            if (tSquadra != null)
            {
                _context.TSquadras.Remove(tSquadra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSquadraExists(int id)
        {
            return _context.TSquadras.Any(e => e.Id == id);
        }
    }
}

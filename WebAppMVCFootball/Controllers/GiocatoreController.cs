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
    public class GiocatoreController : Controller
    {
        private readonly FootballContext _context;

        public GiocatoreController(FootballContext context)
        {
            _context = context;
        }

        // GET: Giocatore
        public async Task<IActionResult> Index()
        {
            var footballContext = _context.TGiocatores.Include(t => t.IdSquadraNavigation);
            return View(await footballContext.ToListAsync());
        }

        // GET: Giocatore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGiocatore = await _context.TGiocatores
                .Include(t => t.IdSquadraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tGiocatore == null)
            {
                return NotFound();
            }

            return View(tGiocatore);
        }

        // GET: Giocatore/Create
        public IActionResult Create()
        {
            ViewData["IdSquadra"] = new SelectList(_context.TSquadras, "Id", "Id");
            return View();
        }

        // POST: Giocatore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Eta,IdSquadra,Nazionalita")] TGiocatore tGiocatore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tGiocatore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSquadra"] = new SelectList(_context.TSquadras, "Id", "Id", tGiocatore.IdSquadra);
            return View(tGiocatore);
        }

        // GET: Giocatore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGiocatore = await _context.TGiocatores.FindAsync(id);
            if (tGiocatore == null)
            {
                return NotFound();
            }
            ViewData["IdSquadra"] = new SelectList(_context.TSquadras, "Id", "Id", tGiocatore.IdSquadra);
            return View(tGiocatore);
        }

        // POST: Giocatore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Eta,IdSquadra,Nazionalita")] TGiocatore tGiocatore)
        {
            if (id != tGiocatore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tGiocatore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGiocatoreExists(tGiocatore.Id))
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
            ViewData["IdSquadra"] = new SelectList(_context.TSquadras, "Id", "Id", tGiocatore.IdSquadra);
            return View(tGiocatore);
        }

        // GET: Giocatore/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tGiocatore = await _context.TGiocatores
                .Include(t => t.IdSquadraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tGiocatore == null)
            {
                return NotFound();
            }

            return View(tGiocatore);
        }

        // POST: Giocatore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tGiocatore = await _context.TGiocatores.FindAsync(id);
            if (tGiocatore != null)
            {
                _context.TGiocatores.Remove(tGiocatore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TGiocatoreExists(int id)
        {
            return _context.TGiocatores.Any(e => e.Id == id);
        }
    }
}

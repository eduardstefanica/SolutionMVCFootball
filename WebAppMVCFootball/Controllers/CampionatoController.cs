using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAppMVCFootball.Models;

namespace WebAppMVCFootball.Controllers
{
    public class CampionatoController : Controller
    {
        private readonly FootballContext _context;
        private readonly ILogger<CampionatoController> _logger;

        public CampionatoController(FootballContext context, ILogger<CampionatoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Campionato
        public async Task<IActionResult> Index()
        {
            // USAGE SERILOG
            List<TCampionato> listCampionato = null;
            try
            {
                listCampionato = await _context.TCampionatos.ToListAsync();
                _logger.LogInformation("Lista dei vari campionati");
                //throw new Exception("Errore!");
            }
            catch (Exception ex)
            {
                string sErr = string.Empty;
                if (ex.InnerException != null)
                {
                    sErr = string.Format("Source: {0}{4}Message: {1}{4}StackTrace: {2}{4}InnerException: {3}{4}", ex.Source, ex.Message, ex.StackTrace, ex.InnerException, System.Environment.NewLine);
                }
                else
                {
                    sErr = string.Format("Source: {0}{3}Message: {1}{3}StackTrace: {2}{3}", ex.Source, ex.Message, ex.StackTrace, System.Environment.NewLine);
                }
                _logger.LogError(sErr);
                throw;
            }
            return View(listCampionato);
            //return View(await _context.TCampionatos.ToListAsync());
        }

        // GET: Campionato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCampionato = await _context.TCampionatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCampionato == null)
            {
                return NotFound();
            }

            return View(tCampionato);
        }

        // GET: Campionato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campionato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Paese,NomeFederazione,IsTopRanking")] TCampionato tCampionato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCampionato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tCampionato);
        }

        // GET: Campionato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCampionato = await _context.TCampionatos.FindAsync(id);
            if (tCampionato == null)
            {
                return NotFound();
            }
            return View(tCampionato);
        }

        // POST: Campionato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Paese,NomeFederazione,IsTopRanking")] TCampionato tCampionato)
        {
            if (id != tCampionato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCampionato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCampionatoExists(tCampionato.Id))
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
            return View(tCampionato);
        }

        // GET: Campionato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCampionato = await _context.TCampionatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCampionato == null)
            {
                return NotFound();
            }

            return View(tCampionato);
        }

        // POST: Campionato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCampionato = await _context.TCampionatos.FindAsync(id);
            if (tCampionato != null)
            {
                _context.TCampionatos.Remove(tCampionato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCampionatoExists(int id)
        {
            return _context.TCampionatos.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;

namespace ProyectoInventarioASP.Controllers
{
    public class MemoriaRamController : Controller
    {
        private readonly ComputadoraContext _context;

        public MemoriaRamController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: MemoriaRam
        public async Task<IActionResult> Index()
        {
              return _context.MemoriasRam != null ? 
                          View(await _context.MemoriasRam.ToListAsync()) :
                          Problem("Entity set 'ComputadoraContext.MemoriasRam'  is null.");
        }

        // GET: MemoriaRam/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MemoriasRam == null)
            {
                return NotFound();
            }

            var memoriaRam = await _context.MemoriasRam
                .FirstOrDefaultAsync(m => m.NumSerieId == id);
            if (memoriaRam == null)
            {
                return NotFound();
            }

            return View(memoriaRam);
        }

        // GET: MemoriaRam/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemoriaRam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumSerieId,Marca,Capacidad,Tecnologia,estado")] MemoriaRam memoriaRam)
        {
            if (memoriaRam != null)
            {
                _context.Add(memoriaRam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memoriaRam);
        }

        // GET: MemoriaRam/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MemoriasRam == null)
            {
                return NotFound();
            }

            var memoriaRam = await _context.MemoriasRam.FindAsync(id);
            if (memoriaRam == null)
            {
                return NotFound();
            }
            return View(memoriaRam);
        }

        // POST: MemoriaRam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumSerieId,Marca,Capacidad,Tecnologia,estado")] MemoriaRam memoriaRam)
        {
            if (id != memoriaRam.NumSerieId)
            {
                return NotFound();
            }

            if (memoriaRam != null)
            {
                try
                {
                    _context.Update(memoriaRam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoriaRamExists(memoriaRam.NumSerieId))
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
            return View(memoriaRam);
        }

        // GET: MemoriaRam/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MemoriasRam == null)
            {
                return NotFound();
            }

            var memoriaRam = await _context.MemoriasRam
                .FirstOrDefaultAsync(m => m.NumSerieId == id);
            if (memoriaRam == null)
            {
                return NotFound();
            }

            return View(memoriaRam);
        }

        // POST: MemoriaRam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MemoriasRam == null)
            {
                return Problem("Entity set 'ComputadoraContext.MemoriasRam'  is null.");
            }
            var memoriaRam = await _context.MemoriasRam.FindAsync(id);
            if (memoriaRam != null)
            {
                _context.MemoriasRam.Remove(memoriaRam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemoriaRamExists(string id)
        {
          return (_context.MemoriasRam?.Any(e => e.NumSerieId == id)).GetValueOrDefault();
        }
    }
}

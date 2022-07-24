using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class DisplayController : Controller
    {
        private readonly ComputadoraContext _context;

        public DisplayController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Display
        public async Task<IActionResult> Index()
        {
            var computadoraContext = _context.Displays.Include(d => d.Computadora);
            return View(await computadoraContext.ToListAsync());
        }

        // GET: Display/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays
                .Include(d => d.Computadora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (display == null)
            {
                return NotFound();
            }

            return View(display);
        }

        // GET: Display/Create
        public IActionResult Create()
        {
            ViewData["ComputadoraId"] = new SelectList(_context.Computadoras, "Id", "Id");
            return View();
        }

        // POST: Display/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumInv,NumSerie,Marca,ComputadoraId,NumInvPc,estado")] Display display)
        {
            if (display != null)
            {
                _context.Add(display);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComputadoraId"] = new SelectList(_context.Computadoras, "Id", "Id", display.ComputadoraId);
            return View(display);
        }

        // GET: Display/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays.FindAsync(id);
            if (display == null)
            {
                return NotFound();
            }
            ViewData["ComputadoraId"] = new SelectList(_context.Computadoras, "Id", "Id", display.ComputadoraId);
            return View(display);
        }

        // POST: Display/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,NumInv,NumSerie,Marca,ComputadoraId,NumInvPc,estado")] Display display)
        {
            if (id != display.Id)
            {
                return NotFound();
            }

            if (display != null)
            {
                try
                {
                    _context.Update(display);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisplayExists(display.Id))
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
            ViewData["ComputadoraId"] = new SelectList(_context.Computadoras, "Id", "Id", display.ComputadoraId);
            return View(display);
        }

        // GET: Display/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays
                .Include(d => d.Computadora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (display == null)
            {
                return NotFound();
            }

            return View(display);
        }

        // POST: Display/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Displays == null)
            {
                return Problem("Entity set 'ComputadoraContext.Displays'  is null.");
            }
            var display = await _context.Displays.FindAsync(id);
            if (display != null)
            {
                _context.Displays.Remove(display);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisplayExists(string id)
        {
          return (_context.Displays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

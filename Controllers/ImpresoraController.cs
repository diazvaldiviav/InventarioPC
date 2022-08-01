using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using Microsoft.AspNetCore.Authorization;
using ProyectoInventarioASP.Models;
using Rotativa.AspNetCore;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class ImpresoraController : Controller
    {
        private readonly ComputadoraContext _context;

        public ImpresoraController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Impresora
        public async Task<IActionResult> Index()
        {
            return _context.Impresoras != null ?
                        View(await _context.Impresoras.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Impresoras'  is null.");
        }

        // GET: Impresora/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Impresoras == null)
            {
                return NotFound();
            }

            var impresora = await _context.Impresoras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impresora == null)
            {
                return NotFound();
            }

            return View(impresora);
        }

        // GET: Impresora/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Impresora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumSerie,NumInv,Marca,estado")] Impresora impresora)
        {
            if (impresora != null)
            {
                _context.Impresoras.Add(impresora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(impresora);
        }

        // GET: Impresora/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Impresoras == null)
            {
                return NotFound();
            }

            var impresora = await _context.Impresoras.FindAsync(id);
            if (impresora == null)
            {
                return NotFound();
            }
            return View(impresora);
        }

        // POST: Impresora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumSerie,NumInv,Marca,estado")] Impresora impresora)
        {
            if (id != impresora.Id)
            {
                return NotFound();
            }

            if (impresora != null)
            {
                try
                {
                    _context.Impresoras.Update(impresora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImpresoraExists(impresora.Id))
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
            return View(impresora);
        }

        // GET: Impresora/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Impresoras == null)
            {
                return NotFound();
            }

            var impresora = await _context.Impresoras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impresora == null)
            {
                return NotFound();
            }

            return View(impresora);
        }

        // POST: Impresora/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Impresoras == null)
            {
                return Problem("Entity set 'ComputadoraContext.Impresoras'  is null.");
            }
            var impresora = await _context.Impresoras.FindAsync(id);
            if (impresora != null)
            {
                _context.Impresoras.Remove(impresora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImpresoraExists(int id)
        {
            return (_context.Impresoras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

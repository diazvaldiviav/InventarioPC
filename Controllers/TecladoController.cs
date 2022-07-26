using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class TecladoController : Controller
    {
        private readonly ComputadoraContext _context;

        public TecladoController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Teclado
        public async Task<IActionResult> Index()
        {
              return _context.Teclados != null ? 
                          View(await _context.Teclados.ToListAsync()) :
                          Problem("Entity set 'ComputadoraContext.Teclados'  is null.");
        }

        // GET: Teclado/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Teclados == null)
            {
                return NotFound();
            }

            var teclado = await _context.Teclados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teclado == null)
            {
                return NotFound();
            }

            return View(teclado);
        }

        // GET: Teclado/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teclado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumSerie,NumInv,Marca,TipoConexion,estado")] Teclado teclado)
        {
            if (teclado != null)
            {
                _context.Add(teclado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teclado);
        }

        // GET: Teclado/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Teclados == null)
            {
                return NotFound();
            }

            var teclado = await _context.Teclados.FindAsync(id);
            if (teclado == null)
            {
                return NotFound();
            }
            return View(teclado);
        }

        // POST: Teclado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,NumSerie,NumInv,Marca,TipoConexion,estado")] Teclado teclado)
        {
            if (id != teclado.Id)
            {
                return NotFound();
            }

            if (teclado != null)
            {
                try
                {
                    _context.Update(teclado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecladoExists(teclado.Id))
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
            return View(teclado);
        }

        // GET: Teclado/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Teclados == null)
            {
                return NotFound();
            }

            var teclado = await _context.Teclados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teclado == null)
            {
                return NotFound();
            }

            return View(teclado);
        }

        // POST: Teclado/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Teclados == null)
            {
                return Problem("Entity set 'ComputadoraContext.Teclados'  is null.");
            }
            var teclado = await _context.Teclados.FindAsync(id);
            if (teclado != null)
            {
                _context.Teclados.Remove(teclado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecladoExists(string id)
        {
          return (_context.Teclados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

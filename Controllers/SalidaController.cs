using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class SalidaController : Controller
    {
        private readonly ComputadoraContext _context;

        public SalidaController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Salida
        public async Task<IActionResult> Index()
        {
            var computadoraContext = _context.Salidas.Include(s => s.entradas);
            return View(await computadoraContext.ToListAsync());
        }

        // GET: Salida/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Salidas == null)
            {
                return NotFound();
            }

            var salida = await _context.Salidas
                .Include(s => s.entradas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salida == null)
            {
                return NotFound();
            }

            return View(salida);
        }

        // GET: Salida/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create(int? id)
        {
            var salida = from sali in _context.Salidas
                         where sali.EntradaId == id
                         select sali;

            if (salida.ToList().Count() != 0)
            {
                return RedirectToAction("RegistroError", "Home");
            }

            var Entrada = await _context.Entradas.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (Entrada == null)
            {
                return View();
            }
            var FechaSalida = new Salida();
            FechaSalida.FechaSalida = DateTime.Now;
            FechaSalida.EntradaId = Entrada.Id;
            FechaSalida.Id = Entrada.Id.ToString();
            return View(FechaSalida);
        }

        // POST: Salida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(string Id, string salida, DateTime FechaSalida, int EntradaId, string observaciones)
        {
            var Salida = new Salida();
            Salida.Id = Id;
            Salida.salida = salida;
            Salida.FechaSalida = FechaSalida;
            Salida.EntradaId = EntradaId;
            Salida.observaciones = observaciones;

            if (Salida != null)
            {
                try
                {
                    if (Salida.EntradaId == null || Salida.FechaSalida == null || Salida.Id == null || Salida.observaciones == null || Salida.salida == null)
                    {
                        ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Id", Salida.EntradaId);
                        return View();
                    }
                    _context.Add(Salida);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Entrada", Salida);
                }
                catch (System.Exception)
                {

                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }

        // GET: Salida/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Salidas == null)
            {
                return NotFound();
            }

            var salida = await _context.Salidas.FindAsync(id);
            if (salida == null)
            {
                return NotFound();
            }
            return View(salida);
        }

        // POST: Salida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string salida, DateTime FechaSalida, int EntradaId, string observaciones)
        {
            var Salida = new Salida();
            Salida.Id = id;
            Salida.salida = salida;
            Salida.FechaSalida = FechaSalida;
            Salida.EntradaId = EntradaId;
            Salida.observaciones = observaciones;
            if (id != Salida.Id)
            {
                return NotFound();
            }

            if (Salida != null)
            {
                try
                {
                    if (Salida.salida == null || Salida.FechaSalida == null || Salida.Id == null || Salida.observaciones == null || Salida.EntradaId == null)
                    {
                        return View(Salida);
                    }
                    _context.Update(Salida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalidaExists(Salida.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Salida);
        }

        // GET: Salida/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Salidas == null)
            {
                return NotFound();
            }

            var salida = await _context.Salidas
                .Include(s => s.entradas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salida == null)
            {
                return NotFound();
            }

            return View(salida);
        }

        // POST: Salida/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Salidas == null)
            {
                return Problem("Entity set 'ComputadoraContext.Salidas'  is null.");
            }
            var salida = await _context.Salidas.FindAsync(id);
            if (salida != null)
            {
                _context.Salidas.Remove(salida);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalidaExists(string id)
        {
            return (_context.Salidas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

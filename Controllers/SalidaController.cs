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
        public IActionResult Create(int? id)
        {

            var entrada = from entr in _context.Entradas
                          where entr.Id == id
                          select entr;

            var salida = from sali in _context.Salidas
                         where sali.EntradaId == id
                         select sali;

            if (salida.ToList().Count() != 0)
            {
                return RedirectToAction("RegistroError", "Home");
            }


            ViewData["EntradaId"] = new SelectList(entrada.ToList(), "Id", "Id");
            var FechaSalida = new Salida();
            FechaSalida.FechaSalida = DateTime.Now;
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
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Id", Salida.EntradaId);
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
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Id", salida.EntradaId);
            return View(salida);
        }

        // POST: Salida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Salida salida)
        {
            if (id != salida.Id)
            {
                return NotFound();
            }

            if (salida != null)
            {
                try
                {
                    if (salida.EntradaId == null || salida.FechaSalida == null || salida.Id == null || salida.observaciones == null || salida.salida == null)
                    {
                        ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Id", salida.EntradaId);
                        return View();
                    }
                    _context.Update(salida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalidaExists(salida.Id))
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
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Id", salida.EntradaId);
            return View(salida);
        }

        // GET: Salida/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
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
        [Authorize(Roles = "admin , lecturaYEscritura")]
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

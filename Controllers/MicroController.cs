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
    public class MicroController : Controller
    {
        private readonly ComputadoraContext _context;

        public MicroController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Micro
        public async Task<IActionResult> Index()
        {
            return _context.MicroProcesadores != null ?
                        View(await _context.MicroProcesadores.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.MicroProcesadores'  is null.");
        }

        // GET: Micro/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MicroProcesadores == null)
            {
                return NotFound();
            }

            var microProcesador = await _context.MicroProcesadores
                .FirstOrDefaultAsync(m => m.NumSerieId == id);
            if (microProcesador == null)
            {
                return NotFound();
            }

            return View(microProcesador);
        }

        // GET: Micro/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Micro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MicroProcesador microProcesador)
        {
            if (microProcesador != null)
            {
                try
                {
                    if (microProcesador.Marca == null || microProcesador.NumSerieId == null || microProcesador.Tecnologia == null)
                    {
                        return View(microProcesador);
                    }
                    _context.Add(microProcesador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception)
                {

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(microProcesador);
        }

        // GET: Micro/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MicroProcesadores == null)
            {
                return NotFound();
            }

            var microProcesador = await _context.MicroProcesadores.FindAsync(id);
            if (microProcesador == null)
            {
                return NotFound();
            }
            return View(microProcesador);
        }

        // POST: Micro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumSerieId,Marca,Tecnologia,estado")] MicroProcesador microProcesador)
        {
            if (id != microProcesador.NumSerieId)
            {
                return NotFound();
            }

            if (microProcesador != null)
            {
                try
                {
                    if (microProcesador.Marca == null || microProcesador.NumSerieId == null || microProcesador.Tecnologia == null)
                    {
                        return View(microProcesador);
                    }
                    _context.Update(microProcesador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MicroProcesadorExists(microProcesador.NumSerieId))
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
            return View(microProcesador);
        }

        // GET: Micro/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MicroProcesadores == null)
            {
                return NotFound();
            }

            var microProcesador = await _context.MicroProcesadores
                .FirstOrDefaultAsync(m => m.NumSerieId == id);
            if (microProcesador == null)
            {
                return NotFound();
            }

            return View(microProcesador);
        }

        // POST: Micro/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MicroProcesadores == null)
            {
                return Problem("Entity set 'ComputadoraContext.MicroProcesadores'  is null.");
            }
            var microProcesador = await _context.MicroProcesadores.FindAsync(id);
            if (microProcesador != null)
            {
                _context.MicroProcesadores.Remove(microProcesador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MicroProcesadorExists(string id)
        {
            return (_context.MicroProcesadores?.Any(e => e.NumSerieId == id)).GetValueOrDefault();
        }
    }
}

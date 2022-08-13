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
using Rotativa.AspNetCore;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class EntradaController : Controller
    {
        private readonly ComputadoraContext _context;

        public EntradaController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Entrada
        public async Task<IActionResult> Index()
        {
            return _context.Entradas != null ?
                        View(await _context.Entradas.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Entradas'  is null.");
        }

        // GET: Entrada/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var BuscarSalida = from salida in _context.Salidas
                               where salida.EntradaId == id
                               select salida;

            var salidafinal = BuscarSalida.ToList().Where(item => item.EntradaId == id).FirstOrDefault();


            var entrada = await _context.Entradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salidafinal != null)
            {
                entrada.salidas.Id = salidafinal.Id;
                entrada.salidas.salida = salidafinal.salida;
                entrada.salidas.observaciones = salidafinal.observaciones;
                entrada.salidas.EntradaId = salidafinal.EntradaId;
            }

            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Entrada/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entrada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Entrada entrada)
        {
            if (entrada != null)
            {
                if (entrada.Lugar == null || entrada.Equipo == null || entrada.Entrega == null || entrada.FechaEntrega == null || entrada.observaciones == null)
                {
                    return View(entrada);
                }
                if (entrada.observaciones.Length > 250)
                {
                   return View(entrada);
                }
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entrada);
        }

        // GET: Entrada/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }
            return View(entrada);
        }

        // POST: Entrada/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Equipo,NumInvEquipo,Entrega,Lugar,FechaEntrega,observaciones")] Entrada entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            if (entrada != null)
            {
                try
                {
                    _context.Update(entrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaExists(entrada.Id))
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
            return View(entrada);
        }

        // GET: Entrada/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entrada/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entradas == null)
            {
                return Problem("Entity set 'ComputadoraContext.Entradas'  is null.");
            }
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //Controlador de impresion

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Print(int id)
        {
            if (id == 0 || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", entrada)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }

        public ActionResult HeaderPdf()
        {
            return View("HeaderPDF");
        }

        public ActionResult FooterPdf()
        {
            return View("FooterPDF");
        }


        //Fin del controlador de impresion

        private bool EntradaExists(int id)
        {
            return (_context.Entradas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

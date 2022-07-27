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
    public class UpsController : Controller
    {
        private readonly ComputadoraContext _context;

        public UpsController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Ups
        public async Task<IActionResult> Index()
        {
            return _context.Upss != null ?
                        View(await _context.Upss.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Upss'  is null.");
        }

        // GET: Ups/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Upss == null)
            {
                return NotFound();
            }

            var ups = await _context.Upss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ups == null)
            {
                return NotFound();
            }

            return View(ups);
        }

        // GET: Ups/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumSerie,NumInv,Marca,estado")] Ups ups)
        {
            if (ups != null)
            {
                _context.Add(ups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ups);
        }

        // GET: Ups/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Upss == null)
            {
                return NotFound();
            }

            var ups = await _context.Upss.FindAsync(id);
            if (ups == null)
            {
                return NotFound();
            }
            return View(ups);
        }

        // POST: Ups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,NumSerie,NumInv,Marca,estado")] Ups ups)
        {
            if (id != ups.Id)
            {
                return NotFound();
            }

            if (ups != null)
            {
                try
                {
                    _context.Update(ups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpsExists(ups.Id))
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
            return View(ups);
        }

        // GET: Ups/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Upss == null)
            {
                return NotFound();
            }

            var ups = await _context.Upss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ups == null)
            {
                return NotFound();
            }

            return View(ups);
        }

        // POST: Ups/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Upss == null)
            {
                return Problem("Entity set 'ComputadoraContext.Upss'  is null.");
            }
            var ups = await _context.Upss.FindAsync(id);
            if (ups != null)
            {
                _context.Upss.Remove(ups);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //Controlador de impresion

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Imprimir()
        {
            return View();
        }

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult ImprimirFilter()
        {
            return View();
        }

        [Authorize(Roles = "admin , lecturaYEscritura")]
        // GET: Customers/ContactPDF
        [HttpPost]
        public async Task<IActionResult> Imprimir(string Id)
        {

            var BuscarMarc = from ups in _context.Upss
                             where ups.Marca == Id
                             select ups;

            var ArrBuscarMarc = BuscarMarc.ToArray();

            if (ArrBuscarMarc.Length != 0)
            {
                return new ViewAsPdf("Imprimir", await BuscarMarc.ToListAsync())
                {
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                };
            }
            else
            {
                if (Id == "activo" || Id == "inactivo")
                {
                    if (Id == "activo")
                    {
                        var BuscarAct = from ups in _context.Upss
                                        where ups.estado == Estado.activo
                                        select ups;


                        var ArrBuscarAct = BuscarAct.ToArray();

                        if (ArrBuscarAct.Length != 0)
                        {
                            return new ViewAsPdf("Imprimir", await BuscarAct.ToListAsync())
                            {
                                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                            };
                        }
                    }
                    else if (Id == "inactivo")
                    {
                        var BuscarInac = from ups in _context.Upss
                                         where ups.estado == Estado.inactivo
                                         select ups;


                        var ArrBuscarInac = BuscarInac.ToArray();

                        if (ArrBuscarInac.Length != 0)
                        {
                            return new ViewAsPdf("Imprimir", await BuscarInac.ToListAsync())
                            {
                                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                            };
                        }
                    }
                }
            }

            return View();

        }

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Print(string id)
        {
            if (id == null || _context.Upss == null)
            {
                return NotFound();
            }

            var ups = await _context.Upss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ups == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", ups)
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



        private bool UpsExists(string id)
        {
            return (_context.Upss?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
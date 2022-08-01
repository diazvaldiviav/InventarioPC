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
using Rotativa.AspNetCore;

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
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Displays == null)
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
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            ViewData["InvPc"] = new SelectList(_context.Computadoras, "NumInv", "NumInv");
            return View();
        }

        // POST: Display/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Display display)
        {
            if (display != null)
            {
                try
                {
                    var idpc = CargarIdPC(display.NumInvPc);

                    display.ComputadoraId = idpc;
                    _context.Add(display);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (SystemException)
                {
                    return RedirectToAction("Index", "Home");

                }


            }
            ViewData["InvPc"] = new SelectList(_context.Computadoras, "NumInv", "NumInv", display.NumInvPc);
            return View(display);
        }

        // GET: Display/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays.FindAsync(id);
            if (display == null)
            {
                return NotFound();
            }
            ViewData["InvPc"] = new SelectList(_context.Computadoras, "NumInv", "NumInv");
            return View(display);
        }

        // POST: Display/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Display display)
        {
            if (id != display.Id)
            {
                return NotFound();
            }

            if (display != null)
            {
                try
                {
                    var idpc = CargarIdPC(display.NumInvPc);

                    display.ComputadoraId = idpc;
                    _context.Update(display);
                    await _context.SaveChangesAsync();
                }
                catch (SystemException)
                {
                    if (!DisplayExists(display.Id))
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
            ViewData["InvPc"] = new SelectList(_context.Computadoras, "NumInv", "NumInv", display.NumInvPc);
            return View(display);
        }

        // GET: Display/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Displays == null)
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
        [Authorize(Roles = "admin , lecturaYEscritura")]
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
        public async Task<IActionResult> Imprimir(string NumInv)
        {

            var BuscarMarc = from mon in _context.Displays
                             where mon.Marca == NumInv
                             select mon;

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
                if (NumInv == "activo" || NumInv == "inactivo")
                {
                    if (NumInv == "activo")
                    {
                        var BuscarAct = from tmon in _context.Displays
                                        where tmon.estado == Estado.activo
                                        select tmon;


                        var ArrBuscarAct = BuscarAct.ToArray();

                        if (ArrBuscarAct.Length != 0)
                        {
                            return new ViewAsPdf("Imprimir", await BuscarAct.ToListAsync())
                            {
                                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                            };
                        }
                    }
                    else if (NumInv == "inactivo")
                    {
                        var BuscarInac = from mon in _context.Displays
                                         where mon.estado == Estado.inactivo
                                         select mon;


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
        public async Task<IActionResult> Print(int id)
        {
            if (id == 0 || _context.Displays == null)
            {
                return NotFound();
            }

            var monitor = await _context.Displays
                .Include(c => c.Computadora)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monitor == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", monitor)
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

        private int CargarIdPC(string inv)
        {
            var BuscarPc = from pc in _context.Computadoras
                           where pc.NumInv == inv
                           select pc.Id;

            var ArrPc = BuscarPc.ToArray();

            return ArrPc[0];
        }

        private bool DisplayExists(int id)
        {
            return (_context.Displays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

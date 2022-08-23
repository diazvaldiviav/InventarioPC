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
    public class CelularController : Controller
    {
        private readonly ComputadoraContext _context;

        public CelularController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Celular
        public async Task<IActionResult> Index()
        {
            return _context.Celulares != null ?
                        View(await _context.Celulares.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Celulares'  is null.");
        }

        // GET: Celular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Celulares == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (celular == null)
            {
                return NotFound();
            }

            return View(celular);
        }

        // GET: Celular/Create

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create(string NombreUsuario)
        {
            if (NombreUsuario == null)
            {
                var Trabajadores = await _context.Usuarios.ToListAsync();
                ViewBag.Trabajadores = Trabajadores;

                return View();
            }
            var esUsuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuario);
            List<Usuario> ListUsuario = new List<Usuario>();
            ListUsuario.Add(esUsuario);
            // ViewData["EntradaId"] = new SelectList(entrada.ToList(), "Id", "Id");
            ViewData["NombreUser"] = new SelectList(ListUsuario, "NombreUsuario", "NombreUsuario");
            return View();
        }

        // POST: Celular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Celular celular)
        {

            if (celular != null)
            {
                try
                {
                    if (celular.estado == null || celular.Id == null || celular.Marca == null || celular.NumInv == null || celular.NumSerie == null || celular.UserName == null || celular.UsuarioId == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", celular.UserName);
                        return View(celular);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == celular.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();

                    celular.UsuarioId = idUser[0];
                    _context.Add(celular);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception)
                {

                    return RedirectToAction("Index", "Home");
                }

            }

            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", celular.UserName);
            return View(celular);

        }

        // GET: Celular/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Celulares == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulares.FindAsync(id);
            if (celular == null)
            {
                return NotFound();
            }
            var Trabajadores = await _context.Usuarios.ToListAsync();
            ViewBag.Trabajadores = Trabajadores;
            return View(celular);
        }

        // POST: Celular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Celular celular)
        {

            if (id != celular.Id)
            {
                return NotFound();
            }

            if (celular != null)
            {
                try
                {
                    if (celular.estado == null || celular.Id == null || celular.Marca == null || celular.NumInv == null || celular.NumSerie == null || celular.UserName == null || celular.UsuarioId == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", celular.UserName);
                        return View(celular);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == celular.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    celular.UsuarioId = idUser[0];
                    _context.Update(celular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CelularExists(celular.Id))
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
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", celular.UserName);
            return View(celular);
        }

        // GET: Celular/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Celulares == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (celular == null)
            {
                return NotFound();
            }

            return View(celular);
        }

        // POST: Celular/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Celulares == null)
            {
                return Problem("Entity set 'ComputadoraContext.Celulares'  is null.");
            }
            var celular = await _context.Celulares.FindAsync(id);
            if (celular != null)
            {
                var nuevaBaja = new Bajas();
                nuevaBaja.NumInv = celular.NumInv;
                nuevaBaja.NumSerie = celular.NumSerie;
                nuevaBaja.Marca = celular.Marca;
                nuevaBaja.Equipo = "Celular";
                nuevaBaja.SerieBoard = "-";
                nuevaBaja.fechaBaja = DateTime.Now;
                _context.Bajas.Add(nuevaBaja);
                _context.Celulares.Remove(celular);
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

            var BuscarMarc = from cel in _context.Celulares
                             where cel.Marca == NumInv
                             select cel;

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
                        var BuscarAct = from cel in _context.Celulares
                                        where cel.estado == Estado.activo
                                        select cel;


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
                        var BuscarInac = from tecl in _context.Teclados
                                         where tecl.estado == Estado.inactivo
                                         select tecl;


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
            if (id == 0 || _context.Celulares == null)
            {
                return NotFound();
            }

            var celular = await _context.Celulares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (celular == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", celular)
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


        private bool CelularExists(int id)
        {
            return (_context.Celulares?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

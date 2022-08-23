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
    public class TelefonoController : Controller
    {
        private readonly ComputadoraContext _context;

        public TelefonoController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Telefono
        public async Task<IActionResult> Index()
        {
            return _context.Telefonos != null ?
                        View(await _context.Telefonos.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Telefonos'  is null.");
        }

        // GET: Telefono/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefono/Create
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


        // POST: Telefono/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            if (telefono != null)
            {
                try
                {
                    if (telefono.Marca == null || telefono.NumInv == null || telefono.NumSerie == null || telefono.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", telefono.UserName);
                        return View(telefono);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == telefono.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();

                    telefono.UsuarioId = idUser[0];
                    _context.Add(telefono);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (System.Exception)
                {

                    return RedirectToAction("Index", "Home");
                }



            }
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", telefono.UserName);
            return View(telefono);
        }

        // GET: Telefono/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            var Trabajadores = await _context.Usuarios.ToListAsync();
            ViewBag.Trabajadores = Trabajadores;
            return View(telefono);
        }

        // POST: Telefono/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Telefono telefono)
        {
            if (id != telefono.Id)
            {
                return NotFound();
            }

            if (telefono != null)
            {
                try
                {
                    if (telefono.Marca == null || telefono.NumInv == null || telefono.NumSerie == null || telefono.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", telefono.UserName);
                        return View(telefono);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == telefono.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    telefono.UsuarioId = idUser[0];
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", telefono.UserName);
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: Telefono/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefono/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Telefonos == null)
            {
                return Problem("Entity set 'ComputadoraContext.Telefonos'  is null.");
            }
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                var nuevaBaja = new Bajas();
                nuevaBaja.NumInv = telefono.NumInv;
                nuevaBaja.NumSerie = telefono.NumSerie;
                nuevaBaja.Marca = telefono.Marca;
                nuevaBaja.Equipo = "Telefono";
                nuevaBaja.fechaBaja = DateTime.Now;
                nuevaBaja.SerieBoard = "-";
                _context.Bajas.Add(nuevaBaja);
                _context.Telefonos.Remove(telefono);
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

            var BuscarMarc = from tel in _context.Telefonos
                             where tel.Marca == NumInv
                             select tel;

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
                        var BuscarAct = from tel in _context.Telefonos
                                        where tel.estado == Estado.activo
                                        select tel;


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
                        var BuscarInac = from tel in _context.Telefonos
                                         where tel.estado == Estado.inactivo
                                         select tel;


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
            if (id == 0 || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", telefono)
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


        private bool TelefonoExists(int id)
        {
            return (_context.Telefonos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

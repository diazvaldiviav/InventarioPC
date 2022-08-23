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
using Rotativa.AspNetCore;

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
        public async Task<IActionResult> Details(int id)
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


        // POST: Teclado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teclado teclado)
        {
            if (teclado != null)
            {
                try
                {
                    if (teclado.Marca == null || teclado.NumInv == null || teclado.NumSerie == null || teclado.TipoConexion == null || teclado.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", teclado.UserName);
                        return View(teclado);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == teclado.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    teclado.UsuarioId = idUser[0];
                    _context.Add(teclado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (SystemException)
                {
                    return RedirectToAction("Index", "Home");
                }



            }
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", teclado.UserName);
            return View(teclado);
        }

        // GET: Teclado/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int id)
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
            var Trabajadores = await _context.Usuarios.ToListAsync();
            ViewBag.Trabajadores = Trabajadores;
            return View(teclado);
        }

        // POST: Teclado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teclado teclado)
        {
            if (id != teclado.Id)
            {
                return NotFound();
            }

            if (teclado != null)
            {
                try
                {
                    if (teclado.Marca == null || teclado.NumInv == null || teclado.NumSerie == null || teclado.TipoConexion == null || teclado.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", teclado.UserName);
                        return View(teclado);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == teclado.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    teclado.UsuarioId = idUser[0];
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
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teclado);
        }

        // GET: Teclado/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(int id)
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
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Teclados == null)
            {
                return Problem("Entity set 'ComputadoraContext.Teclados'  is null.");
            }
            var teclado = await _context.Teclados.FindAsync(id);
            if (teclado != null)
            {
                var nuevaBaja = new Bajas();
                nuevaBaja.NumInv = teclado.NumInv;
                nuevaBaja.NumSerie = teclado.NumSerie;
                nuevaBaja.Marca = teclado.Marca;
                nuevaBaja.Equipo = "Teclado";
                nuevaBaja.fechaBaja = DateTime.Now;
                nuevaBaja.SerieBoard = "-";
                _context.Bajas.Add(nuevaBaja);
                _context.Teclados.Remove(teclado);
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

            var BuscarMarc = from tecl in _context.Teclados
                             where tecl.Marca == NumInv
                             select tecl;

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
                        var BuscarAct = from tecl in _context.Teclados
                                        where tecl.estado == Estado.activo
                                        select tecl;


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
                    else
                    {
                        var BuscarConec = from tecl in _context.Teclados
                                          where tecl.TipoConexion == NumInv
                                          select tecl;

                        var ArrBuscarConec = BuscarConec.ToArray();

                        if (ArrBuscarConec.Length != 0)
                        {
                            return new ViewAsPdf("Imprimir", await BuscarConec.ToListAsync())
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
            if (id == 0 || _context.Teclados == null)
            {
                return NotFound();
            }

            var teclado = await _context.Teclados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teclado == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", teclado)
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





        private bool TecladoExists(int id)
        {
            return (_context.Teclados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

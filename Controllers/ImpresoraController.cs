using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using Microsoft.AspNetCore.Authorization;
using ProyectoInventarioASP.Models;
using Rotativa.AspNetCore;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class ImpresoraController : Controller
    {
        private readonly ComputadoraContext _context;

        public ImpresoraController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Impresora
        public async Task<IActionResult> Index()
        {
            return _context.Impresoras != null ?
                        View(await _context.Impresoras.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Impresoras'  is null.");
        }

        // GET: Impresora/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Impresoras == null)
            {
                return NotFound();
            }

            var impresora = await _context.Impresoras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impresora == null)
            {
                return NotFound();
            }

            return View(impresora);
        }

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


        // POST: Impresora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Impresora impresora)
        {
            if (impresora != null)
            {
                try
                {
                    if (impresora.estado == null || impresora.Id == null || impresora.Marca == null || impresora.NumInv == null || impresora.NumSerie == null || impresora.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", impresora.UserName);
                        return View(impresora);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == impresora.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    impresora.UsuarioId = idUser[0];
                    _context.Impresoras.Add(impresora);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));


                }
                catch (SystemException)
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", impresora.UserName);
            return View(impresora);

        }

        // GET: Impresora/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Impresoras == null)
            {
                return NotFound();
            }

            var impresora = await _context.Impresoras.FindAsync(id);
            if (impresora == null)
            {
                return NotFound();
            }
            var Trabajadores = await _context.Usuarios.ToListAsync();
            ViewBag.Trabajadores = Trabajadores;
            return View(impresora);

        }

        // POST: Impresora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Impresora impresora)
        {
            if (id != impresora.Id)
            {
                return NotFound();
            }

            if (impresora != null)
            {
                 try
                 {
                    if (impresora.estado == null || impresora.Id == null || impresora.Marca == null || impresora.NumInv == null || impresora.NumSerie == null || impresora.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", impresora.UserName);
                        return View(impresora);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == impresora.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    impresora.UsuarioId = idUser[0];
                    _context.Impresoras.Update(impresora);
                    await _context.SaveChangesAsync();
                }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!ImpresoraExists(impresora.Id))
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
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", impresora.UserName);
            return View(impresora);
        }

        // GET: Impresora/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Impresoras == null)
            {
                return NotFound();
            }

            var impresora = await _context.Impresoras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impresora == null)
            {
                return NotFound();
            }

            return View(impresora);
        }

        // POST: Impresora/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Impresoras == null)
            {
                return Problem("Entity set 'ComputadoraContext.Impresoras'  is null.");
            }
            var impresora = await _context.Impresoras.FindAsync(id);
            if (impresora != null)
            {
                var nuevaBaja = new Bajas();
                nuevaBaja.NumInv = impresora.NumInv;
                nuevaBaja.NumSerie = impresora.NumSerie;
                nuevaBaja.Marca = impresora.Marca;
                nuevaBaja.Equipo = "Impresora";
                nuevaBaja.fechaBaja = DateTime.Now;
                nuevaBaja.SerieBoard = "-";
                _context.Bajas.Add(nuevaBaja);
                _context.Impresoras.Remove(impresora);
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

            var BuscarMarc = from impr in _context.Impresoras
                             where impr.Marca == NumInv
                             select impr;

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
                        var BuscarAct = from imp in _context.Impresoras
                                        where imp.estado == Estado.activo
                                        select imp;


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
                        var BuscarInac = from imp in _context.Impresoras
                                         where imp.estado == Estado.inactivo
                                         select imp;


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

            var impresora = await _context.Impresoras
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impresora == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", impresora)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(40, 10, 10, 10)

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

        private bool ImpresoraExists(int id)
        {
            return (_context.Impresoras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

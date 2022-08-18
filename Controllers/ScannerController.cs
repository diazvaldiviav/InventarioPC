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
    public class ScannerController : Controller
    {
        private readonly ComputadoraContext _context;

        public ScannerController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Scanner
        public async Task<IActionResult> Index()
        {
            var computadoraContext = _context.Scanners.Include(s => s.Usuario);
            return View(await computadoraContext.ToListAsync());
        }

        // GET: Scanner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scanners == null)
            {
                return NotFound();
            }

            var scanner = await _context.Scanners
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scanner == null)
            {
                return NotFound();
            }

            return View(scanner);
        }

        // GET: Scanner/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create(string NombreUsuario = null, int? UsuarioId = null)
        {
            if (NombreUsuario == null && UsuarioId == null)
            {
                ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario");
                return View();
            }

            if (NombreUsuario != null)
            {
                var esUsuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.NombreUsuario == NombreUsuario);
                List<Usuario> ListUsuario = new List<Usuario>();
                ListUsuario.Add(esUsuario);
                // ViewData["EntradaId"] = new SelectList(entrada.ToList(), "Id", "Id");
                ViewData["NombreUser"] = new SelectList(ListUsuario, "NombreUsuario", "NombreUsuario");
                return View();
            }

            var esUsuarioId = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == UsuarioId);
            List<Usuario> ListUsuarioPorId = new List<Usuario>();
            ListUsuarioPorId.Add(esUsuarioId);
            // ViewData["EntradaId"] = new SelectList(entrada.ToList(), "Id", "Id");
            ViewData["NombreUser"] = new SelectList(ListUsuarioPorId, "NombreUsuario", "NombreUsuario");
            return View();

        }

        // POST: Scanner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Scanner scanner)
        {
            if (scanner != null)
            {
                try
                {
                    if (scanner.Marca == null || scanner.NumInv == null || scanner.NumInv == null || scanner.NumSerie == null || scanner.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", scanner.UserName);
                        return View(scanner);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == scanner.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    scanner.UsuarioId = idUser[0];
                    _context.Add(scanner);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (SystemException)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", scanner.UserName);
            return View(scanner);
        }

        // GET: Scanner/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scanners == null)
            {
                return NotFound();
            }

            var scanner = await _context.Scanners.FindAsync(id);
            if (scanner == null)
            {
                return NotFound();
            }
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario");
            return View(scanner);
        }

        // POST: Scanner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumSerie,NumInv,Marca,estado,UsuarioId,UserName")] Scanner scanner)
        {
            if (id != scanner.Id)
            {
                return NotFound();
            }

            if (scanner != null)
            {
                try
                {
                    if (scanner.Marca == null || scanner.NumInv == null || scanner.NumInv == null || scanner.NumSerie == null || scanner.UserName == null)
                    {
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", scanner.UserName);
                        return View(scanner);
                    }
                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == scanner.UserName
                                       select user.Id;

                    var idUser = BuscarIdUser.ToArray();
                    scanner.UsuarioId = idUser[0];
                    _context.Update(scanner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScannerExists(scanner.Id))
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
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", scanner.UserName);
            return View(scanner);
        }

        // GET: Scanner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scanners == null)
            {
                return NotFound();
            }

            var scanner = await _context.Scanners
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scanner == null)
            {
                return NotFound();
            }

            return View(scanner);
        }

        // POST: Scanner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scanners == null)
            {
                return Problem("Entity set 'ComputadoraContext.Scanners'  is null.");
            }
            var scanner = await _context.Scanners.FindAsync(id);
            if (scanner != null)
            {
                _context.Scanners.Remove(scanner);
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

            var BuscarMarc = from scan in _context.Scanners
                             where scan.Marca == NumInv
                             select scan;

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
                        var BuscarAct = from scan in _context.Scanners
                                        where scan.estado == Estado.activo
                                        select scan;


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
                        var BuscarInac = from scan in _context.Scanners
                                         where scan.estado == Estado.inactivo
                                         select scan;


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
            if (id == 0 || _context.Scanners == null)
            {
                return NotFound();
            }

            var scanner = await _context.Scanners
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scanner == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", scanner)
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

        private bool ScannerExists(int id)
        {
            return (_context.Scanners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

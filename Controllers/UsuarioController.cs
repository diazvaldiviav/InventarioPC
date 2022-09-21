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
using NuGet.Packaging;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly ComputadoraContext _context;

        public UsuarioController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return _context.Usuarios != null ?
                        View(await _context.Usuarios.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Usuarios'  is null.");
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Usuarios == null)
            {
                return NotFound();
            }
            var monitores = CargarMonitores(id);
            var computadoras = CargarComputadoras(id);
            var teclados = CargarTeclados(id);
            var upss = CargarUps(id);
            var impresoras = CargarImpresoras(id);
            var celulares = CargarCelulares(id);
            var telefonos = CargarTelefono(id);
            var scaners = CargarScanners(id);
            var laptop = CargarLaptop(id);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);

            var modelo = modeloDeUsuario(usuario, monitores, computadoras, teclados, impresoras, upss, celulares, telefonos, scaners, laptop);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(modelo);
        }

        // GET: Usuario/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {
            ViewBag.Areas = AreasDepartamentos.AreasDepartamentos.Areas();
            ViewBag.Departamentos = AreasDepartamentos.AreasDepartamentos.Departamentos();
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (usuario != null)
            {
                try
                {
                    if (usuario.NombreArea == null || usuario.NombreCompleto == null || usuario.NombreDepartamento == null || usuario.NombreUsuario == null || usuario.Cargo == null)
                    {
                        return View(usuario);
                    }
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Areas = AreasDepartamentos.AreasDepartamentos.Areas();
            ViewBag.Departamentos = AreasDepartamentos.AreasDepartamentos.Departamentos();
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {

            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (usuario != null)
            {
                try
                {
                    if (usuario.NombreArea == null || usuario.NombreCompleto == null || usuario.NombreDepartamento == null || usuario.NombreUsuario == null || usuario.Cargo == null)
                    {
                        return View(usuario);
                    }
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null || usuario.NombreUsuario == "Sin Trabajador")
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ComputadoraContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                try
                {
                    _context.Usuarios.Remove(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("BdError", "Home");
                }

            }


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
        public async Task<IActionResult> Imprimir(string NombreCompleto)
        {

            var BuscarDep = from pc in _context.Usuarios
                            where pc.NombreDepartamento == NombreCompleto
                            select pc;

            var ArrBuscarDep = BuscarDep.ToArray();

            if (ArrBuscarDep.Length != 0)
            {
                return new ViewAsPdf("Imprimir", await BuscarDep.ToListAsync())
                {
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                };
            }
            else
            {
                var BuscarArea = from pc in _context.Usuarios
                                 where pc.NombreArea == NombreCompleto
                                 select pc;


                var ArrBuscarArea = BuscarArea.ToArray();

                if (ArrBuscarArea.Length != 0)
                {
                    return new ViewAsPdf("Imprimir", await BuscarArea.ToListAsync())
                    {
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                    };
                }
            }

            return View();

        }

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Print(int id)
        {
            if (id == 0 || _context.Usuarios == null)
            {
                return NotFound();
            }

            var monitores = CargarMonitores(id);
            var computadoras = CargarComputadoras(id);
            var teclados = CargarTeclados(id);
            var upss = CargarUps(id);
            var impresoras = CargarImpresoras(id);
            var celulares = CargarCelulares(id);
            var telefonos = CargarTelefono(id);
            var scaners = CargarScanners(id);
            var laptop = CargarLaptop(id);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);

            var modelo = modeloDeUsuario(usuario, monitores, computadoras, teclados, impresoras, upss, celulares, telefonos, scaners, laptop);


            if (usuario == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", modelo)
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

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private List<Display> CargarMonitores(int id)
        {
            var buscarMon = from user in _context.Displays
                            where user.UsuarioId == id
                            select user;

            return buscarMon.ToList();

        }

        private List<Teclado> CargarTeclados(int id)
        {
            var buscarTecl = from user in _context.Teclados
                             where user.UsuarioId == id
                             select user;
            return buscarTecl.ToList();

        }

        private List<Computadora> CargarComputadoras(int id)
        {
            var buscarPC = from user in _context.Computadoras
                           where user.UsuarioId == id
                           select user;
            return buscarPC.ToList();

        }

        private List<Ups> CargarUps(int id)
        {
            var buscarUps = from user in _context.Upss
                            where user.UsuarioId == id
                            select user;
            return buscarUps.ToList();

        }

        private List<Impresora> CargarImpresoras(int id)
        {
            var buscarImpr = from user in _context.Impresoras
                             where user.UsuarioId == id
                             select user;

            return buscarImpr.ToList();

        }

        private List<Scanner> CargarScanners(int id)
        {
            var buscarscan = from user in _context.Scanners
                             where user.UsuarioId == id
                             select user;

            return buscarscan.ToList();

        }

        private List<Celular> CargarCelulares(int id)
        {
            var buscarcel = from user in _context.Celulares
                            where user.UsuarioId == id
                            select user;

            return buscarcel.ToList();

        }

        private List<Telefono> CargarTelefono(int id)
        {
            var buscartel = from user in _context.Telefonos
                            where user.UsuarioId == id
                            select user;

            return buscartel.ToList();

        }

        private List<Laptop> CargarLaptop(int id)
        {
            var buscarlap = from user in _context.Laptops
                            where user.UsuarioId == id
                            select user;

            return buscarlap.ToList();

        }

        private Usuario modeloDeUsuario(Usuario usuario, List<Display> monitores, List<Computadora> computadoras, List<Teclado> teclados, List<Impresora> impresoras, List<Ups> upss, List<Celular> celular, List<Telefono> telefono, List<Scanner> scan, List<Laptop> laptop)
        {
            var nuevoUser = new Usuario();
            nuevoUser.Monitores = new List<Display>();
            nuevoUser.Impresora = new List<Impresora>();
            nuevoUser.Computadora = new List<Computadora>();
            nuevoUser.Teclado = new List<Teclado>();
            nuevoUser.Ups = new List<Ups>();
            nuevoUser.Celular = new List<Celular>();
            nuevoUser.Telefono = new List<Telefono>();
            nuevoUser.Scanner = new List<Scanner>();
            nuevoUser.Laptop = new List<Laptop>();
            nuevoUser.Id = usuario.Id;
            nuevoUser.NombreUsuario = usuario.NombreUsuario;
            nuevoUser.NombreCompleto = usuario.NombreCompleto;
            nuevoUser.NombreDepartamento = usuario.NombreDepartamento;
            nuevoUser.NombreArea = usuario.NombreArea;
            nuevoUser.Cargo = usuario.Cargo;
            nuevoUser.Monitores.AddRange(monitores);
            nuevoUser.Impresora.AddRange(impresoras);
            nuevoUser.Computadora.AddRange(computadoras);
            nuevoUser.Teclado.AddRange(teclados);
            nuevoUser.Ups.AddRange(upss);
            nuevoUser.Celular.AddRange(celular);
            nuevoUser.Telefono.AddRange(telefono);
            nuevoUser.Scanner.AddRange(scan);
            nuevoUser.Laptop.AddRange(laptop);

            return nuevoUser;
        }

    }
}

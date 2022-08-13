using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using ProyectoInventarioASP.Models;
using Rotativa.AspNetCore;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class ComputadoraController : Controller
    {
        private readonly ComputadoraContext _context;

        public ComputadoraController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Computadora
        public async Task<IActionResult> Index()
        {
            var computadoraContext = _context.Computadoras.Include(c => c.Impresora).Include(c => c.MotherBoard).Include(c => c.Teclado).Include(c => c.Ups);
            return View(await computadoraContext.ToListAsync());
        }

        // GET: Computadora/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Computadoras == null)
            {
                return NotFound();
            }


            var computadora = await _context.Computadoras
                .Include(c => c.Impresora)
                .Include(c => c.MotherBoard)
                .Include(c => c.Teclado)
                .Include(c => c.Ups)
                .FirstOrDefaultAsync(m => m.Id == id);


            var monitores = CargarMonitores(computadora.Id);
            var memorias = CargarMemorias(computadora.MotherBoardId);
            var discos = CargarDiscos(computadora.MotherBoardId);
            var scan = CargarScanner(computadora.UserName);
            computadora.Display = new List<Display>();
            computadora.Discos = new List<DiscoDuro>();
            computadora.Memorias = new List<MemoriaRam>();
            computadora.Scanners = new List<Scanner>();

            computadora.Display.AddRange(monitores);
            computadora.Discos.AddRange(discos);
            computadora.Memorias.AddRange(memorias);
            computadora.Scanners.AddRange(scan);

            if (computadora == null)
            {
                return NotFound();
            }

            return View(computadora);
        }


        // GET: ImprimirFilter
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

            var BuscarDep = from pc in _context.Computadoras
                            where pc.NombreDepartamento == NumInv
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
                var BuscarArea = from pc in _context.Computadoras
                                 where pc.NombreArea == NumInv
                                 select pc;


                var ArrBuscarArea = BuscarArea.ToArray();

                if (ArrBuscarArea.Length != 0)
                {
                    return new ViewAsPdf("Imprimir", await BuscarArea.ToListAsync())
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
                            var BuscarAct = from pc in _context.Computadoras
                                            where pc.estado == Estado.activo
                                            select pc;


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
                            var BuscarInac = from pc in _context.Computadoras
                                             where pc.estado == Estado.inactivo
                                             select pc;


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
            }

            return View();

        }

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Print(int id)
        {
            if (id == 0 || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras
                .Include(c => c.Impresora)
                .Include(c => c.MotherBoard)
                .Include(c => c.Teclado)
                .Include(c => c.Ups)
                .FirstOrDefaultAsync(m => m.Id == id);


            var BuscarBoardID = from pc in _context.MemoriasRam
                                where pc.MotherBoardId == computadora.MotherBoardId
                                select pc.MotherBoardId;

            var boardid = BuscarBoardID.ToArray();

            var monitores = CargarMonitores(computadora.Id);

            var memorias = CargarMemorias(boardid[0]);
            var discos = CargarDiscos(computadora.MotherBoardId);
            var scan = CargarScanner(computadora.UserName);
            computadora.Display = new List<Display>();
            computadora.Discos = new List<DiscoDuro>();
            computadora.Memorias = new List<MemoriaRam>();
            computadora.Scanners = new List<Scanner>();

            computadora.Display.AddRange(monitores);
            computadora.Discos.AddRange(discos);
            computadora.Memorias.AddRange(memorias);
            computadora.Scanners.AddRange(scan);
            if (computadora == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DetailsPrint", computadora)
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




        // GET: Computadora/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public IActionResult Create()
        {

            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv");
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv");
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv");
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario");

            return View();
        }

        // POST: Computadora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Computadora computadora)
        {
            if (computadora != null)
            {
                try
                {
                    if (computadora.estado == null || computadora.Mac == null || computadora.Nombre == null || computadora.NombreArea == null || computadora.NombreDepartamento == null || computadora.NumIp == null || computadora.MotherBoardId == null)
                    {
                        return View(computadora);

                    }

                    //Cargarlos id de impresoras
                    var BuscarIdImpr = from impr in _context.Impresoras
                                       where impr.NumInv == computadora.ImprNumInv
                                       select impr.Id;

                    //Cargar los Id de las Ups
                    var BuscarIdUps = from ups in _context.Upss
                                      where ups.NumInv == computadora.UpsInv
                                      select ups.Id;

                    //Cargar los Id de los teclados
                    var BuscarIdTecl = from tecl in _context.Teclados
                                       where tecl.NumInv == computadora.TeclNumInv
                                       select tecl.Id;

                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == computadora.UserName
                                       select user.Id;
                    //Importar Ids
                    var idImpr = BuscarIdImpr.ToArray();

                    var idUps = BuscarIdUps.ToArray();

                    var idTecl = BuscarIdTecl.ToArray();

                    var idUser = BuscarIdUser.ToArray();

                    var marcaBoard = CargarMarca(computadora.MotherBoardId);

                    var tecMicro = CargarTecnMic(computadora.MotherBoardId);



                    computadora.ImpresoraId = idImpr[0];
                    computadora.UpsId = idUps[0];
                    computadora.TecladoId = idTecl[0];
                    computadora.UsuarioId = idUser[0];
                    computadora.MotherBoardMarca = marcaBoard;
                    computadora.MicroTecn = tecMicro;
                    _context.Add(computadora);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (SystemException)
                {
                    return RedirectToAction("Index", "Home");
                }


            }


            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv", computadora.ImprNumInv);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv", computadora.TeclNumInv);
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv", computadora.UpsInv);
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", computadora.UserName);
            return View(computadora);
        }

        // GET: Computadora/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            if (id == 0 || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras.FindAsync(id);
            if (computadora == null)
            {
                return NotFound();
            }

            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv");
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv");
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv");
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario");
            return View(computadora);
        }

        // POST: Computadora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Computadora computadora)
        {

            if (id != computadora.Id)
            {
                return NotFound();
            }

            if (computadora != null)
            {
                try
                {
                    //Cargarlos id de impresoras
                    var BuscarIdImpr = from impr in _context.Impresoras
                                       where impr.NumInv == computadora.ImprNumInv
                                       select impr.Id;

                    //Cargar los Id de las Ups
                    var BuscarIdUps = from ups in _context.Upss
                                      where ups.NumInv == computadora.UpsInv
                                      select ups.Id;

                    //Cargar los Id de los teclados
                    var BuscarIdTecl = from tecl in _context.Teclados
                                       where tecl.NumInv == computadora.TeclNumInv
                                       select tecl.Id;

                    //Cargar los Id de los usuarios
                    var BuscarIdUser = from user in _context.Usuarios
                                       where user.NombreUsuario == computadora.UserName
                                       select user.Id;
                    //Importar Ids
                    var idImpr = BuscarIdImpr.ToArray();

                    var idUps = BuscarIdUps.ToArray();

                    var idTecl = BuscarIdTecl.ToArray();

                    var idUser = BuscarIdUser.ToArray();

                    var marcaBoard = CargarMarca(computadora.MotherBoardId);

                    var tecmem = CargarTecnMemo(computadora.MotherBoardId);

                    var tecMicro = CargarTecnMic(computadora.MotherBoardId);


                    computadora.ImpresoraId = idImpr[0];
                    computadora.UpsId = idUps[0];
                    computadora.TecladoId = idTecl[0];
                    computadora.UsuarioId = idUser[0];
                    computadora.MotherBoardMarca = marcaBoard;
                    computadora.MicroTecn = tecMicro;
                    _context.Update(computadora);
                    await _context.SaveChangesAsync();
                }
                catch (SystemException)
                {
                    if (!ComputadoraExists(computadora.Id))
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


            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv", computadora.ImprNumInv);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv", computadora.TeclNumInv);
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv", computadora.UpsInv);
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", computadora.UserName);
            return View(computadora);
        }

        // GET: Computadora/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras
                .Include(c => c.Impresora)
                .Include(c => c.MotherBoard)
                .Include(c => c.Teclado)
                .Include(c => c.Ups)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computadora == null)
            {
                return NotFound();
            }

            return View(computadora);
        }

        // POST: Computadora/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Computadoras == null)
            {
                return Problem("Entity set 'ComputadoraContext.Computadoras'  is null.");
            }
            var computadora = await _context.Computadoras.FindAsync(id);
            if (computadora != null)
            {
                _context.Computadoras.Remove(computadora);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputadoraExists(int id)
        {
            return (_context.Computadoras?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string CargarMarca(string id)
        {
            var BuscarMarca = from board in _context.MotherBoards
                              where board.NumSerieId == id
                              select board.Marca;

            var Marca = BuscarMarca.ToArray();


            return Marca[0];


        }

        private string CargarTecnMemo(string id)
        {
            var BuscarTec = from tec in _context.MemoriasRam
                            where tec.MotherBoardId == id
                            select tec.Tecnologia;

            var tecArr = BuscarTec.ToArray();


            return tecArr[0];

        }

        private string CargarTecnMic(string id)
        {
            var Buscarboard = from board in _context.MotherBoards
                              where board.NumSerieId == id
                              select board.MicroProcesadorId;

            var micrArr = Buscarboard.ToArray();

            var BuscarTecMicro = from micro in _context.MicroProcesadores
                                 where micro.NumSerieId == micrArr[0]
                                 select micro.Tecnologia;

            var tecMicro = BuscarTecMicro.ToArray();


            return tecMicro[0];

        }



        private List<Display> CargarMonitores(int id)
        {
            var BuscarMon = from mon in _context.Displays
                            where mon.ComputadoraId == id
                            select mon;

            return BuscarMon.ToList();
        }

        private List<Scanner> CargarScanner(string username)
        {
            var Buscarscan = from scan in _context.Scanners
                             where scan.UserName == username
                             select scan;

            return Buscarscan.ToList();
        }


        private List<DiscoDuro> CargarDiscos(string id)
        {

            var ListTemp = from disco in _context.DiscosDuro
                           where disco.MotherBoardId == id
                           select disco;


            return ListTemp.ToList();


        }


        private List<MemoriaRam> CargarMemorias(string id)
        {

            var ListTemp = from memoria in _context.MemoriasRam
                           where memoria.MotherBoardId == id
                           select memoria;


            return ListTemp.ToList();

        }


    }
}


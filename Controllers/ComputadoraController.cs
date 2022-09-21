using ProyectoInventarioASP.AreasDepartamentos;
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

            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == computadora.MotherBoardId);
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieId == board.MicroProcesadorId);
            var monitores = CargarMonitores(computadora.Id);
            var memorias = CargarMemorias(computadora.MotherBoardId);
            var discos = CargarDiscos(computadora.MotherBoardId);
            var scan = CargarScanner(computadora.UserName);
            computadora.Display = new List<Display>();
            computadora.Discos = new List<DiscoDuro>();
            computadora.Memorias = new List<MemoriaRam>();
            computadora.Scanners = new List<Scanner>();
            computadora.MotherBoard = new MotherBoard();

            computadora.MicroTecn = micro.Tecnologia;
            computadora.Display.AddRange(monitores);
            computadora.Discos.AddRange(discos);
            computadora.Memorias.AddRange(memorias);
            computadora.Scanners.AddRange(scan);
            computadora.MotherBoard.NumSerieBoard = board.NumSerieBoard;
            computadora.MotherBoardMarca = board.Marca;

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
                    else
                    {
                        var pc = await _context.Computadoras.Where(pc => pc.SO == NumInv).ToListAsync();

                        if (pc.Count() != 0)
                        {
                            return new ViewAsPdf("Imprimir", pc)
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

            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == computadora.MotherBoardId);
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieId == board.MicroProcesadorId);
            var monitores = CargarMonitores(computadora.Id);
            var memorias = CargarMemorias(computadora.MotherBoardId);
            var discos = CargarDiscos(computadora.MotherBoardId);
            var scan = CargarScanner(computadora.UserName);
            computadora.Display = new List<Display>();
            computadora.Discos = new List<DiscoDuro>();
            computadora.Memorias = new List<MemoriaRam>();
            computadora.Scanners = new List<Scanner>();
            computadora.MotherBoard = new MotherBoard();

            computadora.MicroTecn = micro.Tecnologia;
            computadora.Display.AddRange(monitores);
            computadora.Discos.AddRange(discos);
            computadora.Memorias.AddRange(memorias);
            computadora.Scanners.AddRange(scan);
            computadora.MotherBoard.NumSerieBoard = board.NumSerieBoard;
            computadora.MotherBoardMarca = board.Marca;
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
        public async Task<IActionResult> Create(string NombreUsuario)
        {
            //listas
            var usuarios = await _context.Usuarios.ToListAsync();
            var upss = await _context.Upss.ToListAsync();
            var MotherBoards = await _context.MotherBoards.ToListAsync();
            var teclados = await _context.Teclados.ToListAsync();
            var impresoras = await _context.Impresoras.ToListAsync();
            ViewBag.Areas = AreasDepartamentos.AreasDepartamentos.Areas();
            ViewBag.Departamentos = AreasDepartamentos.AreasDepartamentos.Departamentos();

            if (NombreUsuario == null)
            {
                //objetos
                var modelo = CargarModelo(upss, MotherBoards, teclados, impresoras, usuarios);
                ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario");
                return View(modelo);
            }

            var Trabajador = await _context.Usuarios.Where(m => m.NombreUsuario == NombreUsuario).ToListAsync();

            var ModeloUnTrabajador = CargarModelo(upss, MotherBoards, teclados, impresoras, Trabajador);

            ViewData["NombreUser"] = new SelectList(Trabajador, "NombreUsuario", "NombreUsuario");
            return View(ModeloUnTrabajador);
        }

        // POST: Computadora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Computadora computadora)
        {
            ViewBag.Areas = AreasDepartamentos.AreasDepartamentos.Areas();
            ViewBag.Departamentos = AreasDepartamentos.AreasDepartamentos.Departamentos();

            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieBoard == computadora.MotherBoardId);
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieId == board.MicroProcesadorId);

            if (computadora != null)
            {
                try
                {
                    if (computadora.estado == null || computadora.Mac == null || computadora.Nombre == null || computadora.NombreArea == null || computadora.NombreDepartamento == null || computadora.NumIp == null || computadora.NumInv == null)
                    {

                        return View(computadora);

                    }
                    if (computadora.Nombre.Length < 13 || computadora.Nombre.Length > 15)
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

                    var marcaBoard = board.Marca;

                    var tecMicro = micro.Tecnologia;



                    computadora.ImpresoraId = idImpr[0];
                    computadora.UpsId = idUps[0];
                    computadora.TecladoId = idTecl[0];
                    computadora.UsuarioId = idUser[0];
                    computadora.MotherBoardMarca = marcaBoard.ToLower();
                    computadora.MicroTecn = tecMicro.ToLower();
                    computadora.MotherBoardId = board.NumSerieId;
                    if (computadora.MotherBoardId == null)
                    {
                        computadora.MotherBoardId = "Sin Board";
                        _context.Computadoras.Add(computadora);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Computadoras.Add(computadora);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (SystemException)
                {
                    return RedirectToAction("Index", "Home");
                }


            }


            return View(computadora);
        }

        // GET: Computadora/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //listas
            var usuarios = await _context.Usuarios.ToListAsync();
            var upss = await _context.Upss.ToListAsync();
            var MotherBoards = await _context.MotherBoards.ToListAsync();
            var teclados = await _context.Teclados.ToListAsync();
            var impresoras = await _context.Impresoras.ToListAsync();


            ViewBag.Areas = AreasDepartamentos.AreasDepartamentos.Areas();
            ViewBag.Departamentos = AreasDepartamentos.AreasDepartamentos.Departamentos();


            if (id == 0 || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras.FindAsync(id);
            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == computadora.MotherBoardId);
            computadora.MotherBoardId = board.NumSerieBoard;
            var modelo = CargarModelo(upss, MotherBoards, teclados, impresoras, usuarios, computadora);

            if (computadora == null)
            {
                return NotFound();
            }


            return View(modelo);
        }

        // POST: Computadora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Computadora computadora)
        {
            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieBoard == computadora.MotherBoardId);
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieId == board.MicroProcesadorId);

            if (id != computadora.Id)
            {
                return NotFound();
            }

            if (computadora != null)
            {
                try
                {
                    if (computadora.estado == null || computadora.Mac == null || computadora.Nombre == null || computadora.NombreArea == null || computadora.NombreDepartamento == null || computadora.NumIp == null || computadora.MotherBoardId == null)
                    {
                        ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv", computadora.ImprNumInv);
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
                        ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv", computadora.TeclNumInv);
                        ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv", computadora.UpsInv);
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", computadora.UserName);
                        return View(computadora);

                    }
                    if (computadora.Nombre.Length < 13 || computadora.Nombre.Length > 15)
                    {
                        ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv", computadora.ImprNumInv);
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
                        ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv", computadora.TeclNumInv);
                        ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv", computadora.UpsInv);
                        ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", computadora.UserName);
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

                    var marcaBoard = board.Marca;

                    var tecMicro = micro.Tecnologia;

                    computadora.ImpresoraId = idImpr[0];
                    computadora.UpsId = idUps[0];
                    computadora.TecladoId = idTecl[0];
                    computadora.UsuarioId = idUser[0];
                    computadora.MotherBoardMarca = marcaBoard.ToLower();
                    computadora.MicroTecn = tecMicro.ToLower();
                    computadora.MotherBoardId = board.NumSerieId;
                    if (computadora.MotherBoardId == null)
                    {
                        computadora.MotherBoardId = "Sin Board";
                        _context.Update(computadora);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Update(computadora);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
        [Authorize(Roles = "admin")]
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

            computadora.MotherBoard = new MotherBoard();

            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == computadora.MotherBoardId);
            var memorias = await _context.MemoriasRam.Where(m => m.MotherBoardId == board.NumSerieId).ToListAsync();
            var discos = await _context.DiscosDuro.Where(d => d.MotherBoardId == board.NumSerieId).ToListAsync();
            var isActiveMemory = memorias.Any(m => m.estado == Estado.activo);
            var isActiveDisk = discos.Any(d => d.estado == Estado.activo);


            computadora.MotherBoard.NumSerieBoard = board.NumSerieBoard;

            if (board.estado == Estado.activo || isActiveMemory || isActiveDisk)
            {
                ViewBag.Message = "No se le puede dar baja a una computadora con un componente activo";
            }
            else if (board.NumSerieBoard == "Sin Board" || memorias.Count() == 0 || discos.Count() == 0)
            {
                ViewBag.Message2 = "A esta computadora le falta un componente";
            }
            else
            {
                ViewBag.Message = null;
                ViewBag.Message2 = null;
            }


            if (computadora == null || computadora.NumInv == "Sin Computadora")
            {
                return NotFound();
            }

            return View(computadora);
        }

        // POST: Computadora/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Computadoras == null)
            {
                return Problem("Entity set 'ComputadoraContext.Computadoras'  is null.");
            }
            var computadora = await _context.Computadoras.FindAsync(id);
            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == computadora.MotherBoardId);
            var memorias = await _context.MemoriasRam.Where(m => m.MotherBoardId == board.NumSerieId).ToListAsync();
            var discos = await _context.DiscosDuro.Where(d => d.MotherBoardId == board.NumSerieId).ToListAsync();
            var isActiveMemory = memorias.Any(m => m.estado == Estado.activo);
            var isActiveDisk = discos.Any(d => d.estado == Estado.activo);
            if (computadora != null)
            {
                var nuevaBaja = new Bajas();
                nuevaBaja.NumInv = computadora.NumInv;
                nuevaBaja.NumSerie = "-";
                nuevaBaja.Marca = "-";
                nuevaBaja.Equipo = "Computadora";
                nuevaBaja.SerieBoard = board.NumSerieBoard;
                nuevaBaja.fechaBaja = DateTime.Now;
                if (board.estado == Estado.activo || isActiveMemory || isActiveDisk)
                {
                    ViewBag.Message = "No se le puede dar baja a una computadora con un componente activo";
                    return RedirectToAction("Delete");
                }
                else if (board.NumSerieBoard == "Sin Board" || memorias.Count() == 0 || discos.Count() == 0)
                {
                    ViewBag.Message2 = "A esta computadora le falta un componente vaya a los detalles para mas informacion";
                    return RedirectToAction("Delete");
                }

                ViewBag.Message = null;
                _context.Bajas.Add(nuevaBaja);
                try
                {
                    _context.Computadoras.Remove(computadora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("BdError", "Home");
                }
            }


            return RedirectToAction(nameof(Index));
        }

        //metodos privados

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

        //usuarios, upss, MotherBoards, teclados, impresoras
        private Computadora CargarModelo(List<Ups> upss, List<MotherBoard> boards, List<Teclado> teclados, List<Impresora> impresoras, List<Usuario> trabajadores, Computadora pc = null)
        {
            //instancia de objeto
            var ModeloComputadora = new Computadora();

            if (pc == null)
            {
                ModeloComputadora.Usuarios = new List<Usuario>();
                ModeloComputadora.Upss = new List<Ups>();
                ModeloComputadora.MotherBoards = new List<MotherBoard>();
                ModeloComputadora.Teclados = new List<Teclado>();
                ModeloComputadora.Impresoras = new List<Impresora>();
                ModeloComputadora.Upss.AddRange(upss);
                ModeloComputadora.MotherBoards.AddRange(boards);
                ModeloComputadora.Teclados.AddRange(teclados);
                ModeloComputadora.Impresoras.AddRange(impresoras);
                ModeloComputadora.Usuarios.AddRange(trabajadores);

                return ModeloComputadora;
            }

            //inicializaciones de objeto
            ModeloComputadora.Usuarios = new List<Usuario>();
            ModeloComputadora.Upss = new List<Ups>();
            ModeloComputadora.MotherBoards = new List<MotherBoard>();
            ModeloComputadora.Teclados = new List<Teclado>();
            ModeloComputadora.Impresoras = new List<Impresora>();
            ModeloComputadora.Upss.AddRange(upss);
            ModeloComputadora.MotherBoards.AddRange(boards);
            ModeloComputadora.Teclados.AddRange(teclados);
            ModeloComputadora.Impresoras.AddRange(impresoras);
            ModeloComputadora.Usuarios.AddRange(trabajadores);
            ModeloComputadora.Id = pc.Id;
            ModeloComputadora.ImpresoraId = pc.ImpresoraId;
            ModeloComputadora.ImprNumInv = pc.ImprNumInv;
            ModeloComputadora.Mac = pc.Mac;
            ModeloComputadora.MicroTecn = pc.MicroTecn;
            ModeloComputadora.MotherBoardId = pc.MotherBoardId;
            ModeloComputadora.MotherBoardMarca = pc.MotherBoardMarca;
            ModeloComputadora.Nombre = pc.Nombre;
            ModeloComputadora.NombreArea = pc.NombreArea;
            ModeloComputadora.NombreDepartamento = pc.NombreDepartamento;
            ModeloComputadora.NumInv = pc.NumInv;
            ModeloComputadora.NumIp = pc.NumIp;
            ModeloComputadora.Sello = pc.Sello;
            ModeloComputadora.SO = pc.SO;
            ModeloComputadora.TecladoId = pc.TecladoId;
            ModeloComputadora.TeclNumInv = pc.TeclNumInv;
            ModeloComputadora.UpsId = pc.UpsId;
            ModeloComputadora.UpsInv = pc.UpsInv;
            ModeloComputadora.UserName = pc.UserName;
            ModeloComputadora.UsuarioId = pc.UsuarioId;
            ModeloComputadora.estado = pc.estado;

            return ModeloComputadora;

        }



    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
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

                var CapDisc = CargarCapDisc(computadora.MotherBoardId);

                var CapMem = CargarCapMem(computadora.MotherBoardId);

                var cabledisc = CargarCableDisco(computadora.MotherBoardId);

                var tecmem = CargarTecnMemo(computadora.MotherBoardId);

                var tecMicro = CargarTecnMic(computadora.MotherBoardId);

                computadora.ImpresoraId = idImpr[0];
                computadora.UpsId = idUps[0];
                computadora.TecladoId = idTecl[0];
                computadora.UsuarioId = idUser[0];
                computadora.MotherBoardMarca = marcaBoard;
                computadora.DiscoDuroCap = CapDisc;
                computadora.MemoriaRamCap = CapMem;
                computadora.MemoriaRamTec = tecmem;
                computadora.DiscoDuroTipoCon = cabledisc;
                computadora.MicroTecn = tecMicro;
                _context.Add(computadora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

                    var CapDisc = CargarCapDisc(computadora.MotherBoardId);

                    var CapMem = CargarCapMem(computadora.MotherBoardId);

                    var cabledisc = CargarCableDisco(computadora.MotherBoardId);

                    var tecmem = CargarTecnMemo(computadora.MotherBoardId);

                    var tecMicro = CargarTecnMic(computadora.MotherBoardId);

                    computadora.ImpresoraId = idImpr[0];
                    computadora.UpsId = idUps[0];
                    computadora.TecladoId = idTecl[0];
                    computadora.UsuarioId = idUser[0];
                    computadora.MotherBoardMarca = marcaBoard;
                    computadora.DiscoDuroCap = CapDisc;
                    computadora.MemoriaRamCap = CapMem;
                    computadora.MemoriaRamTec = tecmem;
                    computadora.DiscoDuroTipoCon = cabledisc;
                    computadora.MicroTecn = tecMicro;
                    _context.Update(computadora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputadoraExists(computadora.Id))
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



        private string CargarCableDisco(string id)
        {
            var BuscarCable = from cable in _context.DiscosDuro
                              where cable.MotherBoardId == id
                              select cable.TipoConexion;

            var cableArr = BuscarCable.ToArray();


            return cableArr[0];


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

        private string CargarCapDisc(string id)
        {
            List<string> ListaDisco = new List<string>();
            var BuscarDisc = from disc in _context.DiscosDuro
                             where disc.MotherBoardId == id
                             select disc.Capacidad;

            ListaDisco.AddRange(BuscarDisc);

            float capacidad = 0;
            foreach (var item in ListaDisco)
            {
                var itemConvertToFloat = float.Parse(item);

                capacidad = capacidad + itemConvertToFloat;
            }

            var capacidadInString = capacidad.ToString();

            return capacidadInString;


        }

        private string CargarCapMem(string id)
        {
            List<string> ListaMem = new List<string>();
            var BuscarMem = from mem in _context.MemoriasRam
                            where mem.MotherBoardId == id
                            select mem.Capacidad;

            ListaMem.AddRange(BuscarMem);

            float capacidad = 0;
            foreach (var item in ListaMem)
            {
                var itemConvertToFloat = float.Parse(item);

                capacidad = capacidad + itemConvertToFloat;
            }

            var capacidadInString = capacidad.ToString();

            return capacidadInString;


        }





    }
}


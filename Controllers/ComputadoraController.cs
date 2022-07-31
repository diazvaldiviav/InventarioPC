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
        public async Task<IActionResult> Imprimir(string Id)
        {

            var BuscarDep = from pc in _context.Computadoras
                            where pc.NombreDepartamento == Id
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
                                 where pc.NombreArea == Id
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
                    if (Id == "activo" || Id == "inactivo")
                    {
                        if (Id == "activo")
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
                        else if (Id == "inactivo")
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

            return new ViewAsPdf("Details", computadora)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
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
            //Estoy creando una lista de string que almacena los id de las board en las pc

            List<string> ListPc = new List<string>();
            var BuscarIdBoard = from board in _context.MotherBoards
                                select board.NumSerieId;

            ListPc.AddRange(BuscarIdBoard);

            //aqui creo otra lista de string que almacena la capacidad de los discos por cada id de board
            var discosCapacidad = CargarCapacidadDisco(ListPc);

            //tengo que sumar las capacidades de los discos


            ViewBag.Cap = new SelectList(discosCapacidad);
            ViewData["TipoConecDisc"] = new SelectList(_context.DiscosDuro, "TipoConexion", "TipoConexion");
            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv");
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
            ViewData["MotherBoardMarca"] = new SelectList(_context.MotherBoards, "Marca", "Marca");
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
            List<string> ListPc = new List<string>();
            var BuscarIdBoard = from board in _context.Computadoras
                                select board.MotherBoardId;

            ListPc.AddRange(BuscarIdBoard);

            //aqui creo otra lista de string que almacena la capacidad de los discos por cada id de board
            var discosCapacidad = CargarCapacidadDisco(ListPc);

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

                computadora.ImpresoraId = idImpr[0];
                computadora.UpsId = idUps[0];
                computadora.TecladoId = idTecl[0];
                computadora.UsuarioId = idUser[0];
                _context.Add(computadora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CapDisc"] = new SelectList(discosCapacidad, "DiscoDuroCap", "DiscoDuroCap", computadora.DiscoDuroCap);
            ViewData["TipoConecDisc"] = new SelectList(_context.DiscosDuro, "TipoConexion", "TipoConexion", computadora.DiscoDuroTipoCon);
            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv", computadora.ImprNumInv);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["MotherBoardMarca"] = new SelectList(_context.MotherBoards, "Marca", "Marca", computadora.MotherBoardMarca);
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv", computadora.TeclNumInv);
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv", computadora.UpsInv);
            ViewData["NombreUser"] = new SelectList(_context.Usuarios, "NombreUsuario", "NombreUsuario", computadora.UserName);
            return View(computadora);
        }

        // GET: Computadora/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            List<string> ListPc = new List<string>();
            var BuscarIdBoard = from pc in _context.Computadoras
                                join board in _context.MotherBoards
                                on pc.MotherBoardId equals board.NumSerieId
                                where pc.MotherBoardId == board.NumSerieId
                                select pc.MotherBoardId;

            ListPc.AddRange(BuscarIdBoard);

            //aqui creo otra lista de string que almacena la capacidad de los discos por cada id de board
            var discosCapacidad = CargarCapacidadDisco(ListPc);

            //tengo que sumar las capacidades de los discos


            if (id == 0 || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras.FindAsync(id);
            if (computadora == null)
            {
                return NotFound();
            }
            ViewBag.Cap = new SelectList(discosCapacidad);
            ViewData["ImpresoraId"] = new SelectList(_context.Impresoras, "Id", "Id");
            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv");
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
            ViewData["MotherBoardMarca"] = new SelectList(_context.MotherBoards, "Marca", "Marca");
            ViewData["TecladoId"] = new SelectList(_context.Teclados, "Id", "Id");
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "NumInv");
            ViewData["UpsId"] = new SelectList(_context.Upss, "Id", "Id");
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "NumInv");
            ViewData["NombreUsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
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

            List<string> ListPc = new List<string>();
            var BuscarIdBoard = from pc in _context.Computadoras
                                join board in _context.MotherBoards
                                on pc.MotherBoardId equals board.NumSerieId
                                where pc.MotherBoardId == board.NumSerieId
                                select pc.MotherBoardId;

            ListPc.AddRange(BuscarIdBoard);

            //aqui creo otra lista de string que almacena la capacidad de los discos por cada id de board
            var discosCapacidad = CargarCapacidadDisco(ListPc);


            if (id != computadora.Id)
            {
                return NotFound();
            }

            if (computadora != null)
            {
                try
                {
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
            ViewBag.Cap = new SelectList(discosCapacidad);
            ViewData["ImpresoraId"] = new SelectList(_context.Impresoras, "Id", "Id", computadora.ImpresoraId);
            ViewData["ImpresoraInv"] = new SelectList(_context.Impresoras, "NumInv", "NumInv", computadora.ImprNumInv);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["MotherBoardMarca"] = new SelectList(_context.MotherBoards, "Marca", "Marca", computadora.MotherBoardMarca);
            ViewData["TecladoId"] = new SelectList(_context.Teclados, "Id", "Id", computadora.TecladoId);
            ViewData["TecladoNumInv"] = new SelectList(_context.Teclados, "NumInv", "Inv", computadora.TeclNumInv);
            ViewData["UpsId"] = new SelectList(_context.Upss, "Id", "Id", computadora.UpsId);
            ViewData["UpsInv"] = new SelectList(_context.Upss, "NumInv", "Inv", computadora.UpsInv);
            ViewData["NombreUsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", computadora.UsuarioId);
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

        private List<float> CargarCapacidadDisco(List<string> idBoards)
        {
            List<String> ListDiscos = new List<string>();
            List<String> ListTemp = new List<string>();
           
            foreach (var disco in idBoards)
            {    
                
                if (ListTemp.Any())
                {
                    var BuscarCap = from disc in _context.DiscosDuro
                                    where disc.MotherBoardId == disco
                                    select disc.Capacidad;

                    ListDiscos.AddRange(BuscarCap);

                }else
                {
                     var BuscarCap = from disc in _context.DiscosDuro
                                    where disc.MotherBoardId == disco
                                    select disc.Capacidad;

                    ListTemp.AddRange(BuscarCap);

                }


            }

            List<float> listSuma = new List<float>();


            float suma = 0;
            foreach (var item in ListDiscos)
            {
                var itemConvertToFloat = float.Parse(item);

                suma = suma + itemConvertToFloat;

                listSuma.Add(itemConvertToFloat);
                listSuma.Add(suma);

            }

            return listSuma;


        }




    }
}


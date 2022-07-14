using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;

namespace ProyectoInventarioASP.Controllers
{
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
            var computadoraContext = _context.Computadoras.Include(c => c.DiscoDuro).Include(c => c.Display).Include(c => c.Impresora).Include(c => c.MicroProcesador).Include(c => c.MotherBoard).Include(c => c.Teclado).Include(c => c.Usuario);
            return View(await computadoraContext.ToListAsync());
        }

        public IActionResult Filter()
        {
            return View();
        }


        //Interaccion con el formulario de busqueda a travez del metodo post
        [HttpPost]
        //pasamos el inv primero como parametro
        public IActionResult Filter(string NumInvId)
        {
            try
            {


                //aqui buscamos el id a traves del contexto
                var validacion = _context.Computadoras.Find(NumInvId);
                //valido el dato que me pasaron como parametro es null si no es null pues lo que nos entraron fue el id
                if (validacion != null)
                {
                    //si no es null hacemos la consulta a la base de datos buscando el id
                    var buscadorPC = from pc in _context.Computadoras
                                     where pc.NumInvId == NumInvId
                                     select pc;

                    //aqui devolvemos nuestro resultado a la vista
                    return View("Index", buscadorPC);
                }
                else
                {
                    //validacion del nombre
                    IEnumerable<Computadora> BuscarPc = from pc in _context.Computadoras
                                                        where pc.Nombre.ToUpper() == NumInvId.ToUpper()
                                                        select pc;

                    var validacionNombre = BuscarPc.ToArray();

                    if (validacionNombre.Length != 0)
                    {
                        return View("Details", BuscarPc);
                    }
                    else
                    {
                        //validacion del nombre del departamento
                        IEnumerable<Computadora> BuscarNomDep = from pc in _context.Computadoras
                                                                where pc.NombreDepartamento.ToUpper() == NumInvId.ToUpper()
                                                                select pc;

                        var validacionNomDep = BuscarNomDep.ToArray();

                        if (validacionNomDep.Length != 0)
                        {
                            return View("Index", BuscarNomDep.ToList());
                        }
                        else
                        {
                            //validacion del nombre area
                            IEnumerable<Computadora> BuscarNomArea = from pc in _context.Computadoras
                                                                     where pc.NombreArea.ToUpper() == NumInvId.ToUpper()
                                                                     select pc;

                            var validacionNomArea = BuscarNomArea.ToArray();

                            if (validacionNomArea.Length != 0)
                            {
                                return View("Index", BuscarNomArea.ToList());
                            }

                            else
                            {
                                //validacion del nombre del usuario
                                IEnumerable<Computadora> BuscarNomUsuario = from pc in _context.Computadoras
                                                                            where pc.NombreUsuarioId.ToLower() == NumInvId.ToLower()
                                                                            select pc;

                                var validacionNomUsua = BuscarNomUsuario.ToArray();

                                if (validacionNomUsua.Length != 0)
                                {
                                    return View("Details", BuscarNomUsuario.FirstOrDefault());
                                }
                                //Aqui comienzo a filtrar a travez de las relaciones de otras tablas
                                //Comienzo validando la memoria Ram a travez del id y la tecnologia
                                else
                                {
                                    //Comienzo por la capacidad
                                    //Selecciono el id a travez de la Capacidad
                                    var BuscarMem = from mem in _context.MemoriasRam
                                                    where mem.Capacidad == NumInvId
                                                    select mem.NumSerieId;

                                    //Paso el id o los ids que obtengo a un array
                                    var RamString = BuscarMem.ToArray();

                                    //valido que el array tenga algo
                                    if (RamString.Length != 0)
                                    {

                                        List<Computadora> NuevaLista = new List<Computadora>();
                                        //si el array tiene algun objeto lo recorro para iterar por cada objeto 
                                        for (int i = 0; i < RamString.Length; i++)
                                        {
                                            var JoinPcRam = from pc in _context.Computadoras
                                                            join ram in _context.MemoriasRam
                                                            on pc.MemoriaRamId equals ram.NumSerieId
                                                            where pc.MemoriaRamId == RamString[i]
                                                            select pc;
                                            //guardamos los objetos en una lista creada anteriormente
                                            NuevaLista.AddRange(JoinPcRam);
                                        }
                                        //aqui le muestro el resultado de la lista a la vista
                                        return View("Index", NuevaLista);


                                    }
                                    else
                                    {
                                        var BuscarMemTec = from mem in _context.MemoriasRam
                                                           where mem.Tecnologia.ToUpper() == NumInvId.ToUpper()
                                                           select mem.NumSerieId;

                                        var RamStringTec = BuscarMemTec.ToArray();

                                        if (RamStringTec.Length != 0)
                                        {
                                            List<Computadora> NuevaListaTec = new List<Computadora>();

                                            for (int i = 0; i < RamStringTec.Length; i++)
                                            {
                                                var JoinPcRamTec = from pc in _context.Computadoras
                                                                   join ram in _context.MemoriasRam
                                                                   on pc.MemoriaRamId equals ram.NumSerieId
                                                                   where pc.MemoriaRamId == RamStringTec[i]
                                                                   select pc;

                                                NuevaListaTec.AddRange(JoinPcRamTec);
                                            }

                                            return View("Index", NuevaListaTec);

                                        }
                                        //A partir de aqui comenzare a validar por disco duro
                                        //por capacidad al igual que la Ram 
                                        else
                                        {
                                            var BuscarDiscCap = from disc in _context.DiscosDuro
                                                                where disc.Capacidad.ToUpper() == NumInvId.ToUpper()
                                                                select disc.NumSerieId;

                                            var ArrayDisc = BuscarDiscCap.ToArray();

                                            if (ArrayDisc.Length != 0)
                                            {
                                                List<Computadora> NuevaListaPcDisc = new List<Computadora>();
                                                for (int i = 0; i < ArrayDisc.Length; i++)
                                                {
                                                    var JoinPcDisc = from pc in _context.Computadoras
                                                                     join disc in _context.DiscosDuro
                                                                     on pc.DiscoDuroId equals disc.NumSerieId
                                                                     where pc.DiscoDuroId == ArrayDisc[i]
                                                                     select pc;


                                                    NuevaListaPcDisc.AddRange(JoinPcDisc);
                                                }

                                                return View("Index", NuevaListaPcDisc);
                                            }
                                            //Validacion del micro por tecnologia
                                            else
                                            {
                                                var BuscapMicroTec = from micro in _context.MicroProcesadores
                                                                     where micro.Tecnologia.ToUpper() == NumInvId.ToUpper()
                                                                     select micro.NumSerieId;

                                                var ArrayMicro = BuscapMicroTec.ToArray();

                                                if (ArrayMicro.Length != 0)
                                                {
                                                    List<Computadora> NuevaListMicPc = new List<Computadora>();
                                                    for (int i = 0; i < ArrayMicro.Length; i++)
                                                    {
                                                        var JoinMicroPc = from pc in _context.Computadoras
                                                                          join micro in _context.MicroProcesadores
                                                                          on pc.MicroProcesadorId equals micro.NumSerieId
                                                                          where pc.MicroProcesadorId == ArrayMicro[i]
                                                                          select pc;

                                                        NuevaListMicPc.AddRange(JoinMicroPc);
                                                    }

                                                    return View("Index", NuevaListMicPc);
                                                }
                                                else
                                                {
                                                    var BuscarBoard = from board in _context.MotherBoards
                                                                      where board.Marca.ToUpper() == NumInvId.ToUpper()
                                                                      select board.NumSerieId;

                                                    var ArrayBoard = BuscarBoard.ToArray();

                                                    if (ArrayBoard.Length != 0)
                                                    {
                                                        List<Computadora> PcBoardList = new List<Computadora>();
                                                        for (int i = 0; i < ArrayBoard.Length; i++)
                                                        {
                                                            var JoinPcBoard = from pc in _context.Computadoras
                                                                              join board in _context.MotherBoards
                                                                              on pc.MotherBoardId equals board.NumSerieId
                                                                              where pc.MotherBoardId == ArrayBoard[i]
                                                                              select pc;

                                                            PcBoardList.AddRange(JoinPcBoard);
                                                        }

                                                        return View("Index", PcBoardList);
                                                    }
                                                    else
                                                    {
                                                        var buscarMonitor = from monitor in _context.Displays
                                                                            where monitor.NumInvId == NumInvId
                                                                            select monitor.NumInvId;

                                                        var ArrayMon = buscarMonitor.ToArray();

                                                        if (ArrayMon.Length != 0)
                                                        {
                                                            var JoinMonPc = from pc in _context.Computadoras
                                                                            join mon in _context.Displays
                                                                            on pc.MonitorId equals mon.NumInvId
                                                                            where pc.MonitorId == ArrayMon[0]
                                                                            select pc;

                                                            return View("Index", JoinMonPc.ToList());
                                                        }
                                                        else
                                                        {
                                                            var buscarimp = from imp in _context.Impresoras
                                                                            where imp.NumInvId == NumInvId
                                                                            select imp.NumInvId;

                                                            var ArrayImp = buscarimp.ToArray();
                                                            if (ArrayImp.Length != 0)
                                                            {
                                                                List<Computadora> ListPCImp = new List<Computadora>();


                                                                var JoinImpPc = from pc in _context.Computadoras
                                                                                join imp in _context.Impresoras
                                                                                on pc.ImpresoraId equals imp.NumInvId
                                                                                where pc.ImpresoraId == ArrayImp[0]
                                                                                select pc;

                                                                ListPCImp.AddRange(JoinImpPc);

                                                                return View("Index", ListPCImp);
                                                            }
                                                            else
                                                            {

                                                                List<Computadora> ListTecl = new List<Computadora>();
                                                                var buscartecl = from tecl in _context.Computadoras
                                                                                 where tecl.TecladoId == NumInvId
                                                                                 select tecl;


                                                                var ArrTecl = buscartecl.ToArray();

                                                                if (ArrTecl.Length != 0)
                                                                {
                                                                    ListTecl.AddRange(buscartecl);
                                                                    return View("Index", ListTecl);
                                                                }


                                                            }
                                                        }

                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                return View();
            }

            return View();
        }


        // GET: Computadora/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras
                .Include(c => c.DiscoDuro)
                .Include(c => c.Display)
                .Include(c => c.Impresora)
                .Include(c => c.MemoriaRam)
                .Include(c => c.MicroProcesador)
                .Include(c => c.MotherBoard)
                .Include(c => c.Teclado)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.NumInvId == id);
            if (computadora == null)
            {
                return NotFound();
            }

            return View(computadora);
        }

        // GET: Computadora/Create
        public IActionResult Create()
        {
            ViewData["DiscoDuroId"] = new SelectList(_context.DiscosDuro, "NumSerieId", "NumSerieId");
            ViewData["MonitorId"] = new SelectList(_context.Displays, "NumInvId", "NumInvId");
            ViewData["ImpresoraId"] = new SelectList(_context.Impresoras, "NumInvId", "NumInvId");
            ViewData["MemoriaRamId"] = new SelectList(_context.MemoriasRam, "NumSerieId", "NumSerieId");
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId");
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
            ViewData["TecladoId"] = new SelectList(_context.Teclados, "NumInvId", "NumInvId");
            ViewData["NombreUsuarioId"] = new SelectList(_context.Usuarios, "NombreUsuarioId", "NombreUsuarioId");
            return View();
        }

        // POST: Computadora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumInvId,NombreDepartamento,NombreArea,Nombre,estado,MemoriaRamId,Mac,NumIp,ImpresoraId,NombreUsuarioId,DiscoDuroId,MicroProcesadorId,MotherBoardId,MonitorId,TecladoId")] Computadora computadora)
        {
            if (computadora != null)
            {
                _context.Add(computadora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscoDuroId"] = new SelectList(_context.DiscosDuro, "NumSerieId", "NumSerieId", computadora.DiscoDuroId);
            ViewData["MonitorId"] = new SelectList(_context.Displays, "NumInvId", "NumInvId", computadora.MonitorId);
            ViewData["ImpresoraId"] = new SelectList(_context.Impresoras, "NumInvId", "NumInvId", computadora.ImpresoraId);
            ViewData["MemoriaRamId"] = new SelectList(_context.MemoriasRam, "NumSerieId", "NumSerieId", computadora.MemoriaRamId);
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", computadora.MicroProcesadorId);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["TecladoId"] = new SelectList(_context.Teclados, "NumInvId", "NumInvId", computadora.TecladoId);
            ViewData["NombreUsuarioId"] = new SelectList(_context.Usuarios, "NombreUsuarioId", "NombreUsuarioId", computadora.NombreUsuarioId);
            return View(computadora);
        }

        // GET: Computadora/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras.FindAsync(id);
            if (computadora == null)
            {
                return NotFound();
            }
            ViewData["DiscoDuroId"] = new SelectList(_context.DiscosDuro, "NumSerieId", "NumSerieId", computadora.DiscoDuroId);
            ViewData["MonitorId"] = new SelectList(_context.Displays, "NumInvId", "NumInvId", computadora.MonitorId);
            ViewData["ImpresoraId"] = new SelectList(_context.Impresoras, "NumInvId", "NumInvId", computadora.ImpresoraId);
            ViewData["MemoriaRamId"] = new SelectList(_context.MemoriasRam, "NumSerieId", "NumSerieId", computadora.MemoriaRamId);
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", computadora.MicroProcesadorId);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["TecladoId"] = new SelectList(_context.Teclados, "NumInvId", "NumInvId", computadora.TecladoId);
            ViewData["NombreUsuarioId"] = new SelectList(_context.Usuarios, "NombreUsuarioId", "NombreUsuarioId", computadora.NombreUsuarioId);
            return View(computadora);
        }

        // POST: Computadora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumInvId,NombreDepartamento,NombreArea,Nombre,estado,MemoriaRamId,Mac,NumIp,ImpresoraId,NombreUsuarioId,DiscoDuroId,MicroProcesadorId,MotherBoardId,MonitorId,TecladoId")] Computadora computadora)
        {
            if (id != computadora.NumInvId)
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
                    if (!ComputadoraExists(computadora.NumInvId))
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
            ViewData["DiscoDuroId"] = new SelectList(_context.DiscosDuro, "NumSerieId", "NumSerieId", computadora.DiscoDuroId);
            ViewData["MonitorId"] = new SelectList(_context.Displays, "NumInvId", "NumInvId", computadora.MonitorId);
            ViewData["ImpresoraId"] = new SelectList(_context.Impresoras, "NumInvId", "NumInvId", computadora.ImpresoraId);
            ViewData["MemoriaRamId"] = new SelectList(_context.MemoriasRam, "NumSerieId", "NumSerieId", computadora.MemoriaRamId);
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", computadora.MicroProcesadorId);
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", computadora.MotherBoardId);
            ViewData["TecladoId"] = new SelectList(_context.Teclados, "NumInvId", "NumInvId", computadora.TecladoId);
            ViewData["NombreUsuarioId"] = new SelectList(_context.Usuarios, "NombreUsuarioId", "NombreUsuarioId", computadora.NombreUsuarioId);
            return View(computadora);
        }

        // GET: Computadora/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Computadoras == null)
            {
                return NotFound();
            }

            var computadora = await _context.Computadoras
                .Include(c => c.DiscoDuro)
                .Include(c => c.Display)
                .Include(c => c.Impresora)
                .Include(c => c.MemoriaRam)
                .Include(c => c.MicroProcesador)
                .Include(c => c.MotherBoard)
                .Include(c => c.Teclado)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.NumInvId == id);
            if (computadora == null)
            {
                return NotFound();
            }

            return View(computadora);
        }

        // POST: Computadora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
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

        private bool ComputadoraExists(string id)
        {
            return (_context.Computadoras?.Any(e => e.NumInvId == id)).GetValueOrDefault();
        }
    }
}

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
    public class BajasController : Controller
    {
        private readonly ComputadoraContext _context;

        public BajasController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: Bajas
        public async Task<IActionResult> Index()
        {
            return _context.Bajas != null ?
                        View(await _context.Bajas.ToListAsync()) :
                        Problem("Entity set 'ComputadoraContext.Bajas'  is null.");
        }

        // GET: Bajas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bajas == null)
            {
                return NotFound();
            }

            var bajas = await _context.Bajas
                .FirstOrDefaultAsync(m => m.id == id);

            var boardId = CargarMotherboardId(bajas.NumInv);
            var marcaBoard = CargarMotherboardMarca(boardId);
            var discos = CargarDisco(boardId);
            var memorias = CargarMemorias(boardId);
            var microId = CargarMicroId(bajas.NumInv);
            var microTecn = CargarTecnMicro(microId);

            var modeloBaja = baja(bajas.id, bajas.Equipo, bajas.fechaBaja, bajas.NumInv, bajas.NumSerie, bajas.Marca, discos, memorias, boardId, microTecn, marcaBoard);

            if (bajas == null)
            {
                return NotFound();
            }

            return View(modeloBaja);
        }





        // GET: Bajas/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bajas == null)
            {
                return NotFound();
            }

            var bajas = await _context.Bajas.FindAsync(id);
            if (bajas == null)
            {
                return NotFound();
            }
            return View(bajas);
        }

        // POST: Bajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bajas bajas)
        {
            if (id != bajas.id)
            {
                return NotFound();
            }

            if (bajas != null)
            {
                try
                {
                    _context.Update(bajas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BajasExists(bajas.id))
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
            return View(bajas);
        }

        // GET: Bajas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bajas == null)
            {
                return NotFound();
            }

            var bajas = await _context.Bajas
                .FirstOrDefaultAsync(m => m.id == id);

            var boardId = CargarMotherboardId(bajas.NumInv);
            var marcaBoard = CargarMotherboardMarca(boardId);
            var discos = CargarDisco(boardId);
            var memorias = CargarMemorias(boardId);
            var microId = CargarMicroId(bajas.NumInv);
            var microTecn = CargarTecnMicro(microId);

            var modeloBaja = baja(bajas.id, bajas.Equipo, bajas.fechaBaja, bajas.NumInv, bajas.NumSerie, bajas.Marca, discos, memorias, boardId, microTecn, marcaBoard);

            if (bajas == null)
            {
                return NotFound();
            }

            return View(modeloBaja);
        }

        // POST: Bajas/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bajas == null)
            {
                return Problem("Entity set 'ComputadoraContext.Bajas'  is null.");
            }
            var bajas = await _context.Bajas.FindAsync(id);
            if (bajas != null)
            {
                _context.Bajas.Remove(bajas);
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
        public async Task<IActionResult> Imprimir(string Equipo)
        {

            var BuscarEquipos = from baja in _context.Bajas
                                where baja.Equipo == Equipo
                                select baja;

            var ArrBuscarEquipos = BuscarEquipos.ToArray();

            if (ArrBuscarEquipos.Length != 0)
            {
                return new ViewAsPdf("Imprimir", await BuscarEquipos.ToListAsync())
                {
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                };
            }

            return View();

        }

        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Print(int? id)
        {
            if (id == null || _context.Bajas == null)
            {
                return NotFound();
            }

            var bajas = await _context.Bajas
                .FirstOrDefaultAsync(m => m.id == id);

            var boardId = CargarMotherboardId(bajas.NumInv);
            var marcaBoard = CargarMotherboardMarca(boardId);
            var discos = CargarDisco(boardId);
            var memorias = CargarMemorias(boardId);
            var microId = CargarMicroId(bajas.NumInv);
            var microTecn = CargarTecnMicro(microId);

            var modeloBaja = baja(bajas.id, bajas.Equipo, bajas.fechaBaja, bajas.NumInv, bajas.NumSerie, bajas.Marca, discos, memorias, boardId, microTecn, marcaBoard);

            if (bajas == null)
            {
                return NotFound();
            }


            return new ViewAsPdf("DetailsPrint", modeloBaja)
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


        //metodos privados

        private bool BajasExists(int id)
        {
            return (_context.Bajas?.Any(e => e.id == id)).GetValueOrDefault();
        }

        private string CargarMotherboardId(string invPc)
        {
            //var usuariofinal = await _context.Users.FirstOrDefaultAsync(m => m.username == _user.username && m.password == _user.password);
            var Computadora = _context.Bajas.FirstOrDefault(c => c.NumInv == invPc);
            var MotherBoard = _context.MotherBoards.FirstOrDefault(b => b.NumSerieId == Computadora.SerieBoard);
            if (MotherBoard == null)
            {
                return "-";
            }

            return MotherBoard.NumSerieId.ToString();
        }

        private string CargarMotherboardMarca(string idBoard)
        {
            //var usuariofinal = await _context.Users.FirstOrDefaultAsync(m => m.username == _user.username && m.password == _user.password);
            var MotherBoard = _context.MotherBoards.FirstOrDefault(b => b.NumSerieId == idBoard);
            if (MotherBoard == null)
            {
                return "-";
            }

            return MotherBoard.Marca;
        }

        private string CargarMicroId(string invPc)
        {
            //var usuariofinal = await _context.Users.FirstOrDefaultAsync(m => m.username == _user.username && m.password == _user.password);
            var Computadora = _context.Bajas.FirstOrDefault(c => c.NumInv == invPc);

            var MotherBoard = _context.MotherBoards.FirstOrDefault(b => b.NumSerieId == Computadora.SerieBoard);
            if (MotherBoard == null)
            {
                return "-";
            }

            return MotherBoard.MicroProcesadorId;
        }

        private string CargarTecnMicro(string id)
        {
            //var usuariofinal = await _context.Users.FirstOrDefaultAsync(m => m.username == _user.username && m.password == _user.password);
            var Micro = _context.MicroProcesadores.FirstOrDefault(c => c.NumSerieId == id);
            if (Micro == null)
            {
                return "-";
            }

            return Micro.Tecnologia.ToString();
        }

        private List<DiscoDuro> CargarDisco(string boardSerie)
        {
            var discos = _context.DiscosDuro.Where(d => d.MotherBoardId == boardSerie);

            return discos.ToList();
        }

        private List<MemoriaRam> CargarMemorias(string boardSerie)
        {

            var memorias = _context.MemoriasRam.Where(d => d.MotherBoardId == boardSerie);

            return memorias.ToList();
        }


        private Bajas baja(
                           int id,
                           string Equipo,
                          DateTime fecha,
                          string NumInv,
                          string Serial = "-",
                          string Marca = "-",
                          List<DiscoDuro> discos = null,
                          List<MemoriaRam> memorias = null,
                          string boardserie = "-",
                          string tecnMicro = "-",
                          string marcaBoard = "-"
                        )
        {

            var nuevaBaja = new Bajas();
            nuevaBaja.Discos = new List<DiscoDuro>();
            nuevaBaja.Memorias = new List<MemoriaRam>();
            nuevaBaja.Micro = new MicroProcesador();
            nuevaBaja.MotherBoard = new MotherBoard();
            nuevaBaja.id = id;
            nuevaBaja.Equipo = Equipo.ToLower();
            nuevaBaja.fechaBaja = fecha;
            nuevaBaja.NumInv = NumInv;
            nuevaBaja.NumSerie = Serial.ToLower();
            nuevaBaja.Marca = Marca.ToLower();
            nuevaBaja.SerieBoard = boardserie.ToLower();
            nuevaBaja.Micro.Tecnologia = tecnMicro.ToLower();
            nuevaBaja.MotherBoard.Marca = marcaBoard.ToLower();
            nuevaBaja.Discos.AddRange(discos);
            nuevaBaja.Memorias.AddRange(memorias);
            return nuevaBaja;
        }


    }
}

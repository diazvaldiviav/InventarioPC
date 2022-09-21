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

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class MotherBoardController : Controller
    {
        private readonly ComputadoraContext _context;

        public MotherBoardController(ComputadoraContext context)
        {
            _context = context;
        }


        // GET: MotherBoard
        public async Task<IActionResult> Index()
        {
            var computadoraContext = _context.MotherBoards.Include(m => m.Micro);
            return View(await computadoraContext.ToListAsync());
        }

        // GET: MotherBoard/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MotherBoards == null)
            {
                return NotFound();
            }

            var motherBoard = await _context.MotherBoards
                .Include(m => m.Micro)
                .FirstOrDefaultAsync(m => m.NumSerieId == id);

            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieId == motherBoard.MicroProcesadorId);

            motherBoard.computadora = new Computadora();
            motherBoard.Discos = new List<DiscoDuro>();
            motherBoard.Memorias = new List<MemoriaRam>();
            motherBoard.baja = new Bajas();
            motherBoard.Micro = new MicroProcesador();
            motherBoard.Micro.NumSerieMicro = micro.NumSerieMicro;
            motherBoard.Micro.Marca = micro.Marca;

            var computadora = await _context.Computadoras.FirstOrDefaultAsync(pc => pc.MotherBoardId == motherBoard.NumSerieId);
            var memorias = await _context.MemoriasRam.Where(m => m.MotherBoardId == motherBoard.NumSerieId).ToListAsync();
            var discos = await _context.DiscosDuro.Where(d => d.MotherBoardId == motherBoard.NumSerieId).ToListAsync();
            var baja = await _context.Bajas.FirstOrDefaultAsync(b => b.SerieBoard == motherBoard.NumSerieBoard);
            if (BoardSerieExistsActive(motherBoard.NumSerieId) || BoardSerieExistsBaja(motherBoard.NumSerieBoard))
            {

                if (computadora != null)
                {
                    motherBoard.computadora.NumInv = computadora.NumInv;
                    motherBoard.computadora.estado = computadora.estado;
                    motherBoard.computadora.Id = computadora.Id;
                    motherBoard.Memorias = memorias;
                    motherBoard.Discos = discos;
                    motherBoard.baja.SerieBoard = "-";
                    return View(motherBoard);
                }
                if (baja != null)
                {
                    motherBoard.baja.SerieBoard = baja.SerieBoard;
                    motherBoard.baja.NumInv = baja.NumInv;
                    motherBoard.computadora.NumInv = "Sin Computadora";
                    // motherBoard.computadora.Id = computadora.Id;
                    motherBoard.Memorias = memorias;
                    motherBoard.Discos = discos;
                    return View(motherBoard);
                }

            }
            if (motherBoard == null)
            {
                return NotFound();
            }

            return View(motherBoard);
        }

        // GET: MotherBoard/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create()
        {
            var micros = await _context.MicroProcesadores.ToListAsync();
            ViewBag.Micros = micros;
            return View();
        }

        // POST: MotherBoard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MotherBoard motherBoard)
        {
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieMicro == motherBoard.MicroProcesadorId);
            motherBoard.NumSerieId = Guid.NewGuid().ToString();
            motherBoard.Marca = motherBoard.Marca.ToLower();
            motherBoard.MicroProcesadorId = micro.NumSerieId;




            if (motherBoard != null)
            {
                try
                {

                    if (motherBoard.Marca == null || motherBoard.MicroProcesadorId == null || motherBoard.NumSerieId == null || motherBoard.invPc == null)
                    {
                        return View(motherBoard);
                    }

                    if (motherBoard.NumSerieBoard == null)
                    {
                        motherBoard.NumSerieBoard = "Vacio por ahora";
                        _context.Add(motherBoard);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    _context.Add(motherBoard);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception)
                {

                    return RedirectToAction("Index", "Home");
                }

            }
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", motherBoard.MicroProcesadorId);
            return View(motherBoard);
        }

        // GET: MotherBoard/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MotherBoards == null)
            {
                return NotFound();
            }

            var motherBoard = await _context.MotherBoards.FindAsync(id);
            var micros = await _context.MicroProcesadores.ToListAsync();
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieId == motherBoard.MicroProcesadorId);
            motherBoard.MicroProcesadorId = micro.NumSerieMicro;
            ViewBag.Micros = micros;
            if (motherBoard == null)
            {
                return NotFound();
            }
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", motherBoard.MicroProcesadorId);
            return View(motherBoard);
        }

        // POST: MotherBoard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, MotherBoard motherBoard)
        {
            var micro = await _context.MicroProcesadores.FirstOrDefaultAsync(m => m.NumSerieMicro == motherBoard.MicroProcesadorId);
            if (id != motherBoard.NumSerieId)
            {
                return NotFound();
            }

            if (motherBoard != null)
            {
                try
                {
                    if (motherBoard.Marca == null || motherBoard.MicroProcesadorId == null || motherBoard.NumSerieId == null)
                    {
                        ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", motherBoard.MicroProcesadorId);
                        return View(motherBoard);
                    }
                    motherBoard.Marca = motherBoard.Marca.ToLower();
                    motherBoard.MicroProcesadorId = micro.NumSerieId;
                    _context.Update(motherBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotherBoardExists(motherBoard.NumSerieId))
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
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", motherBoard.MicroProcesadorId);
            return View(motherBoard);
        }

        // GET: MotherBoard/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MotherBoards == null)
            {
                return NotFound();
            }

            var motherBoard = await _context.MotherBoards
                .Include(m => m.Micro)
                .FirstOrDefaultAsync(m => m.NumSerieId == id);

            if (motherBoard == null || motherBoard.NumSerieId == "Sin Board")
            {
                return NotFound();
            }

            return View(motherBoard);
        }

        // POST: MotherBoard/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MotherBoards == null)
            {
                return Problem("Entity set 'ComputadoraContext.MotherBoards'  is null.");
            }
            var motherBoard = await _context.MotherBoards.FindAsync(id);
            if (motherBoard != null)
            {

                try
                {
                    _context.MotherBoards.Remove(motherBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction("BdError", "Home");
                }
            }


            return RedirectToAction(nameof(Index));
        }

        public List<MemoriaRam> CargarMemorias(string id)
        {
            List<MemoriaRam> ListaFinal = new List<MemoriaRam>();

            var ListTemp = from mem in _context.MemoriasRam
                           where mem.MotherBoardId == id
                           select mem;


            ListaFinal.AddRange(ListTemp);

            return ListaFinal;

        }

        private bool MotherBoardExists(string id)
        {
            return (_context.MotherBoards?.Any(e => e.NumSerieId == id)).GetValueOrDefault();
        }

        private bool BoardSerieExistsActive(string numSerieId)
        {
            return (_context.Computadoras?.Any(pc => pc.MotherBoardId == numSerieId)).GetValueOrDefault();
        }

        private bool BoardSerieExistsBaja(string numSerieId)
        {
            return (_context.Bajas?.Any(pc => pc.SerieBoard == numSerieId)).GetValueOrDefault();
        }
    }
}

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
    public class MemoriaRamController : Controller
    {
        private readonly ComputadoraContext _context;

        public MemoriaRamController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: MemoriaRam
        public async Task<IActionResult> Index()
        {
            var memoria = _context.MemoriasRam.Include(m => m.MotherBoard);
            return View(await memoria.ToListAsync());
        }

        // GET: MemoriaRam/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MemoriasRam == null)
            {
                return NotFound();
            }

            var memoriaRam = await _context.MemoriasRam
                .Include(m => m.MotherBoard)
                .FirstOrDefaultAsync(m => m.KayNumSerieId == id);
            memoriaRam.computadora = new Computadora();
            memoriaRam.baja = new Bajas();

            var computadora = await _context.Computadoras.FirstOrDefaultAsync(pc => pc.MotherBoardId == memoriaRam.MotherBoardId);
            var baja = await _context.Bajas.FirstOrDefaultAsync(b => b.SerieBoard == memoriaRam.MotherBoardId);         
            if (computadora != null)
            {
                memoriaRam.computadora.NumInv = computadora.NumInv;
                memoriaRam.baja.SerieBoard = "-"; 
                return View(memoriaRam);
            }
            if (baja != null)
            {
               memoriaRam.baja.SerieBoard = baja.SerieBoard;
               memoriaRam.baja.NumInv = baja.NumInv;   
               memoriaRam.computadora.NumInv = "Sin Computadora";
               return View(memoriaRam);
            }
            if (memoriaRam == null)
            {
                return NotFound();
            }

            return View(memoriaRam);
        }

        // GET: DiscoDuro/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create(string MotherBoardId = null, string SerieBoard = null)
        {
            ViewBag.MotherBoards = await _context.MotherBoards.ToListAsync();
            var memoria = new MemoriaRam();
            if (MotherBoardId == null && SerieBoard == null)
            {
                return View();
            }
            if (MotherBoardId != null)
            {
                var esBoard = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == MotherBoardId);
                memoria.MotherBoardId = esBoard.NumSerieId;
                return View(memoria);
            }

            var esBoardBaja = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == SerieBoard);
            memoria.MotherBoardId = esBoardBaja.NumSerieId;
            return View(memoria);

        }

        // POST: MemoriaRam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemoriaRam memoriaRam)
        {
            if (memoriaRam != null)
            {
                try
                {
                    if (memoriaRam.Capacidad == null || memoriaRam.KayNumSerieId == null || memoriaRam.Marca == null || memoriaRam.MotherBoardId == null || memoriaRam.Tecnologia == null)
                    {
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", memoriaRam.MotherBoardId);
                        return View(memoriaRam);
                    }
                    memoriaRam.Capacidad = memoriaRam.Capacidad.ToLower();
                    memoriaRam.Marca = memoriaRam.Marca.ToLower();
                    memoriaRam.Tecnologia = memoriaRam.Tecnologia.ToLower();
                    _context.Add(memoriaRam);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception)
                {

                    return RedirectToAction("Index", "Home");
                }

            }
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", memoriaRam.MotherBoardId);
            return View(memoriaRam);
        }

        // GET: MemoriaRam/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MemoriasRam == null)
            {
                return NotFound();
            }

            var memoriaRam = await _context.MemoriasRam.FindAsync(id);
            if (memoriaRam == null)
            {
                return NotFound();
            }
            ViewBag.MotherBoards = await _context.MotherBoards.ToListAsync();
            return View(memoriaRam);
        }

        // POST: MemoriaRam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("KayNumSerieId,Marca,Capacidad,Tecnologia,MotherBoardId,estado")] MemoriaRam memoriaRam)
        {
            if (id != memoriaRam.KayNumSerieId)
            {
                return NotFound();
            }

            if (memoriaRam != null)
            {
                try
                {
                    if (memoriaRam.Capacidad == null || memoriaRam.KayNumSerieId == null || memoriaRam.Marca == null || memoriaRam.MotherBoardId == null || memoriaRam.Tecnologia == null)
                    {
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", memoriaRam.MotherBoardId);
                        return View(memoriaRam);
                    }
                    memoriaRam.Capacidad = memoriaRam.Capacidad.ToLower();
                    memoriaRam.Marca = memoriaRam.Marca.ToLower();
                    memoriaRam.Tecnologia = memoriaRam.Tecnologia.ToLower();
                    _context.Update(memoriaRam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoriaRamExists(memoriaRam.KayNumSerieId))
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
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", memoriaRam.MotherBoardId);
            return View(memoriaRam);
        }

        // GET: MemoriaRam/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MemoriasRam == null)
            {
                return NotFound();
            }

            var memoriaRam = await _context.MemoriasRam
                .Include(m => m.MotherBoard)
                .FirstOrDefaultAsync(m => m.KayNumSerieId == id);
            if (memoriaRam == null)
            {
                return NotFound();
            }

            return View(memoriaRam);
        }

        // POST: MemoriaRam/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MemoriasRam == null)
            {
                return Problem("Entity set 'ComputadoraContext.MemoriasRam'  is null.");
            }
            var memoriaRam = await _context.MemoriasRam.FindAsync(id);
            if (memoriaRam != null)
            {
                _context.MemoriasRam.Remove(memoriaRam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool MemoriaRamExists(string id)
        {
            return (_context.MemoriasRam?.Any(e => e.KayNumSerieId == id)).GetValueOrDefault();
        }

    }
}

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
            if (motherBoard == null)
            {
                return NotFound();
            }

            return View(motherBoard);
        }

        // GET: MotherBoard/Create
        public IActionResult Create()
        {
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId");
            return View();
        }

        // POST: MotherBoard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumSerieId,Marca,MicroProcesadorId,estado")] MotherBoard motherBoard)
        {
            if (motherBoard != null)
            {
                _context.Add(motherBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", motherBoard.MicroProcesadorId);
            return View(motherBoard);
        }

        // GET: MotherBoard/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MotherBoards == null)
            {
                return NotFound();
            }

            var motherBoard = await _context.MotherBoards.FindAsync(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumSerieId,Marca,MicroProcesadorId,estado")] MotherBoard motherBoard)
        {
            if (id != motherBoard.NumSerieId)
            {
                return NotFound();
            }

            if (motherBoard != null)
            {
                try
                {
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
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MicroProcesadorId"] = new SelectList(_context.MicroProcesadores, "NumSerieId", "NumSerieId", motherBoard.MicroProcesadorId);
            return View(motherBoard);
        }

        // GET: MotherBoard/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MotherBoards == null)
            {
                return NotFound();
            }

            var motherBoard = await _context.MotherBoards
                .Include(m => m.Micro)
                .FirstOrDefaultAsync(m => m.NumSerieId == id);
            if (motherBoard == null)
            {
                return NotFound();
            }

            return View(motherBoard);
        }

        // POST: MotherBoard/Delete/5
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
                _context.MotherBoards.Remove(motherBoard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotherBoardExists(string id)
        {
          return (_context.MotherBoards?.Any(e => e.NumSerieId == id)).GetValueOrDefault();
        }
    }
}

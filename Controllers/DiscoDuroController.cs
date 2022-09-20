using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using ProyectoInventarioASP.Models;
using Rotativa.AspNetCore;

namespace ProyectoInventarioASP.Controllers
{
    [Authorize]
    public class DiscoDuroController : Controller
    {
        private readonly ComputadoraContext _context;

        public DiscoDuroController(ComputadoraContext context)
        {
            _context = context;
        }

        // GET: DiscoDuro
        public async Task<IActionResult> Index()
        {
            var disco = _context.DiscosDuro.Include(d => d.motherBoard);
            return View(await disco.ToListAsync());
        }

        // GET: DiscoDuro/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DiscosDuro == null)
            {
                return NotFound();
            }

            var discoDuro = await _context.DiscosDuro
                .Include(d => d.motherBoard)
                .FirstOrDefaultAsync(m => m.NumSerieId == id);

            discoDuro.computadora = new Computadora();
            discoDuro.baja = new Bajas();
            discoDuro.motherBoard = new MotherBoard();
            
            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == discoDuro.MotherBoardId);
            discoDuro.motherBoard.NumSerieBoard = board.NumSerieBoard;
            var computadora = await _context.Computadoras.FirstOrDefaultAsync(pc => pc.MotherBoardId == discoDuro.MotherBoardId);
            var baja = await _context.Bajas.FirstOrDefaultAsync(b => b.SerieBoard == discoDuro.motherBoard.NumSerieBoard);
            if (computadora != null)
            {
                discoDuro.computadora.NumInv = computadora.NumInv;
                discoDuro.computadora.estado = computadora.estado;
                discoDuro.baja.SerieBoard = "-";
                return View(discoDuro);
            }
            if (baja != null)
            {
                discoDuro.baja.SerieBoard = baja.SerieBoard;
                discoDuro.baja.NumInv = baja.NumInv;
                discoDuro.computadora.NumInv = "Sin Computadora";
                return View(discoDuro);
            }

            if (discoDuro == null)
            {
                return NotFound();
            }

            return View(discoDuro);
        }

        // GET: DiscoDuro/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create(int? Id = null, string SerieBoard = null)
        {
            ViewBag.MotherBoards = await _context.MotherBoards.ToListAsync();
            var disco = new DiscoDuro();

            if (Id == null && SerieBoard == null)
            {
                //ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
                return View();
            }
            if (Id != null)
            {
                var computadora = await _context.Computadoras.FirstOrDefaultAsync(pc => pc.Id == Id);
                var esBoard = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == computadora.MotherBoardId);
                disco.MotherBoardId = esBoard.NumSerieBoard;
                return View(disco);
            }

            //List<MotherBoard> boardParaBaja = new List<MotherBoard>();
            var esBoardBaja = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == SerieBoard);
            //boardParaBaja.Add(esBoardBaja);
            //ViewData["MotherBoardId"] = new SelectList(boardParaBaja, "NumSerieId", "NumSerieId");
            disco.MotherBoardId = esBoardBaja.NumSerieBoard;
            return View(disco);

        }

        // POST: DiscoDuro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscoDuro discoDuro)
        {
            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieBoard == discoDuro.MotherBoardId);
            discoDuro.NumSerieId = Guid.NewGuid().ToString();
            discoDuro.Marca = discoDuro.Marca.ToLower();
            discoDuro.Capacidad = discoDuro.Capacidad.ToLower();
            discoDuro.TipoConexion = discoDuro.TipoConexion.ToLower();
            discoDuro.MotherBoardId = board.NumSerieId;

            try
            {
                if (discoDuro != null)
                {
                    if (discoDuro.Capacidad == null || discoDuro.Marca == null || discoDuro.MotherBoardId == null || discoDuro.NumSerieId == null || discoDuro.TipoConexion == null || discoDuro.estado == null || discoDuro.invPc == null)
                    {
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", discoDuro.MotherBoardId);
                        return View(discoDuro);
                    }

                    if (discoDuro.MotherBoardId == null)
                    {
                        discoDuro.MotherBoardId = "Sin Board";
                        _context.Add(discoDuro);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }

                    _context.Add(discoDuro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (System.Exception)
            {

                return RedirectToAction("Index", "Home");
            }

            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", discoDuro.MotherBoardId);
            return View(discoDuro);
        }

        // GET: DiscoDuro/Edit/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DiscosDuro == null)
            {
                return NotFound();
            }

            var discoDuro = await _context.DiscosDuro.FindAsync(id);
            var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieId == discoDuro.MotherBoardId);
            discoDuro.MotherBoardId = board.NumSerieBoard;
            if (discoDuro == null)
            {
                return NotFound();
            }
            ViewBag.MotherBoards = await _context.MotherBoards.ToListAsync();
            return View(discoDuro);
        }

        // POST: DiscoDuro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DiscoDuro discoDuro)
        {
           var board = await _context.MotherBoards.FirstOrDefaultAsync(b => b.NumSerieBoard == discoDuro.MotherBoardId);
            if (id != discoDuro.NumSerieId)
            {
                return NotFound();
            }

            if (discoDuro != null)
            {
                try
                {
                    if (discoDuro.Capacidad == null || discoDuro.Marca == null || discoDuro.MotherBoardId == null || discoDuro.NumSerieId == null || discoDuro.TipoConexion == null || discoDuro.estado == null)
                    {
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", discoDuro.MotherBoardId);
                        return View(discoDuro);
                    }
                    discoDuro.Marca = discoDuro.Marca.ToLower();
                    discoDuro.Capacidad = discoDuro.Capacidad.ToLower();
                    discoDuro.TipoConexion = discoDuro.TipoConexion.ToLower();
                    discoDuro.MotherBoardId = board.NumSerieId;
                    _context.Update(discoDuro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscoDuroExists(discoDuro.NumSerieId))
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
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", discoDuro.MotherBoardId);
            return View(discoDuro);
        }

        // GET: DiscoDuro/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DiscosDuro == null)
            {
                return NotFound();
            }

            var discoDuro = await _context.DiscosDuro
                .Include(d => d.motherBoard)
                .FirstOrDefaultAsync(m => m.NumSerieId == id);
            if (discoDuro == null)
            {
                return NotFound();
            }

            return View(discoDuro);
        }

        // POST: DiscoDuro/Delete/5
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DiscosDuro == null)
            {
                return Problem("Entity set 'ComputadoraContext.DiscosDuro'  is null.");
            }
            var discoDuro = await _context.DiscosDuro.FindAsync(id);
            if (discoDuro != null)
            {
                _context.DiscosDuro.Remove(discoDuro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscoDuroExists(string id)
        {
            return (_context.DiscosDuro?.Any(e => e.NumSerieId == id)).GetValueOrDefault();
        }
    }
}

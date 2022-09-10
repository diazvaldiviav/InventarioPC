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

            var computadora = await _context.Computadoras.FirstOrDefaultAsync(pc => pc.MotherBoardId == discoDuro.MotherBoardId);
            var baja = await _context.Bajas.FirstOrDefaultAsync(b => b.SerieBoard == discoDuro.MotherBoardId);         
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
        public async Task<IActionResult> Create(string MotherBoardId = null, string SerieBoard = null)
        {
            ViewBag.MotherBoards = await _context.MotherBoards.ToListAsync();
            var disco = new DiscoDuro();

            if (MotherBoardId == null && SerieBoard == null)
            {
                //ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
                return View();
            }
            if (MotherBoardId != null)
            {
                var esBoard = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == MotherBoardId);
                //List<MotherBoard> board = new List<MotherBoard>();
                //board.Add(esBoard);
                //ViewData["MotherBoardId"] = new SelectList(board, "NumSerieId", "NumSerieId");
                disco.MotherBoardId = esBoard.NumSerieId;
                return View(disco);
            }

            //List<MotherBoard> boardParaBaja = new List<MotherBoard>();
            var esBoardBaja = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == SerieBoard);
            //boardParaBaja.Add(esBoardBaja);
            //ViewData["MotherBoardId"] = new SelectList(boardParaBaja, "NumSerieId", "NumSerieId");
            disco.MotherBoardId = esBoardBaja.NumSerieId;
            return View(disco);

        }

        // POST: DiscoDuro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin , lecturaYEscritura")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumSerieId,Marca,TipoConexion,Capacidad,MotherBoardId,estado")] DiscoDuro discoDuro)
        {
            try
            {
                if (discoDuro != null)
                {
                    if (discoDuro.Capacidad == null || discoDuro.Marca == null || discoDuro.MotherBoardId == null || discoDuro.NumSerieId == null || discoDuro.TipoConexion == null || discoDuro.estado == null)
                    {
                        ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", discoDuro.MotherBoardId);
                        return View(discoDuro);
                    }
                    discoDuro.Marca = discoDuro.Marca.ToLower();
                    discoDuro.Capacidad = discoDuro.Capacidad.ToLower();
                    discoDuro.TipoConexion = discoDuro.TipoConexion.ToLower();
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
        public async Task<IActionResult> Edit(string id, [Bind("NumSerieId,Marca,TipoConexion,Capacidad,MotherBoardId,estado")] DiscoDuro discoDuro)
        {
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

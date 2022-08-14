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
            if (discoDuro == null)
            {
                return NotFound();
            }

            return View(discoDuro);
        }

        // GET: DiscoDuro/Create
        [Authorize(Roles = "admin , lecturaYEscritura")]
        public async Task<IActionResult> Create(string MotherBoardId)
        {
            if (MotherBoardId == null)
            {
                ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId");
                return View();
            }
            List<MotherBoard> board = new List<MotherBoard>();
            var esBoard = await _context.MotherBoards.FirstOrDefaultAsync(m => m.NumSerieId == MotherBoardId);
            board.Add(esBoard);
            ViewData["MotherBoardId"] = new SelectList(board, "NumSerieId", "NumSerieId");
            return View();
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
            ViewData["MotherBoardId"] = new SelectList(_context.MotherBoards, "NumSerieId", "NumSerieId", discoDuro.MotherBoardId);
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

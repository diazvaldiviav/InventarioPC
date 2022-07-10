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
            var computadoraContext = _context.Computadoras.Include(c => c.DiscoDuro).Include(c => c.Display).Include(c => c.Impresora).Include(c => c.MemoriaRam).Include(c => c.MicroProcesador).Include(c => c.MotherBoard).Include(c => c.Teclado).Include(c => c.Usuario);
            return View(await computadoraContext.ToListAsync());
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
            if (ModelState.IsValid)
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

            if (ModelState.IsValid)
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

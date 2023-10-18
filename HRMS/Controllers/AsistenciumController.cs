using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class AsistenciumController : Controller
    {
        private readonly HrmssisContext _context;

        public AsistenciumController(HrmssisContext context)
        {
            _context = context;
        }

        // GET: Asistencium
        public async Task<IActionResult> Index()
        {
            var hrmssisContext = _context.Asistencia.Include(a => a.IdCedulaNavigation);
            return View(await hrmssisContext.ToListAsync());
        }

        // GET: Asistencium/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencium = await _context.Asistencia
                .Include(a => a.IdCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdCedula == id);
            if (asistencium == null)
            {
                return NotFound();
            }

            return View(asistencium);
        }

        // GET: Asistencium/Create
        public IActionResult Create()
        {
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula");
            return View();
        }

        // POST: Asistencium/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCedula,Entrada,Salida,Calificacion")] Asistencium asistencium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula", asistencium.IdCedula);
            return View(asistencium);
        }

        // GET: Asistencium/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencium = await _context.Asistencia.FindAsync(id);
            if (asistencium == null)
            {
                return NotFound();
            }
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula", asistencium.IdCedula);
            return View(asistencium);
        }

        // POST: Asistencium/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCedula,Entrada,Salida,Calificacion")] Asistencium asistencium)
        {
            if (id != asistencium.IdCedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciumExists(asistencium.IdCedula))
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
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula", asistencium.IdCedula);
            return View(asistencium);
        }

        // GET: Asistencium/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencium = await _context.Asistencia
                .Include(a => a.IdCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdCedula == id);
            if (asistencium == null)
            {
                return NotFound();
            }

            return View(asistencium);
        }

        // POST: Asistencium/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Asistencia == null)
            {
                return Problem("Entity set 'HrmssisContext.Asistencia'  is null.");
            }
            var asistencium = await _context.Asistencia.FindAsync(id);
            if (asistencium != null)
            {
                _context.Asistencia.Remove(asistencium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciumExists(long id)
        {
          return (_context.Asistencia?.Any(e => e.IdCedula == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Controllers
{
    [Authorize]
    public class AsistenciaEmpleadoController : Controller
    {
        private readonly HrmssisContext _context;

        public AsistenciaEmpleadoController(HrmssisContext context)
        {
            _context = context;
        }

        // GET: AsistenciaEmpleado
        public async Task<IActionResult> Index()
        {
            var hrmssisContext = _context.Asistencia.Include(a => a.IdCedulaNavigation);
            return View(await hrmssisContext.ToListAsync());
        }

        // GET: AsistenciaEmpleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencium = await _context.Asistencia
                .Include(a => a.IdCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdAsis == id);
            if (asistencium == null)
            {
                return NotFound();
            }

            return View(asistencium);
        }

        // GET: AsistenciaEmpleado/Create
        public IActionResult Create()
        {
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula");
            return View();
        }

        // POST: AsistenciaEmpleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCedula,Entrada,IdAsis")] Asistencium asistencium)
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

        // GET: AsistenciaEmpleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: AsistenciaEmpleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCedula,Entrada,IdAsis")] Asistencium asistencium)
        {
            if (id != asistencium.IdAsis)
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
                    if (!AsistenciumExists(asistencium.IdAsis))
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

        // GET: AsistenciaEmpleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asistencia == null)
            {
                return NotFound();
            }

            var asistencium = await _context.Asistencia
                .Include(a => a.IdCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdAsis == id);
            if (asistencium == null)
            {
                return NotFound();
            }

            return View(asistencium);
        }

        // POST: AsistenciaEmpleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool AsistenciumExists(int id)
        {
          return (_context.Asistencia?.Any(e => e.IdAsis == id)).GetValueOrDefault();
        }
    }
}

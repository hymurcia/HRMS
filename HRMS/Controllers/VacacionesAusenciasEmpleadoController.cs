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
    public class VacacionesAusenciasEmpleadoController : Controller
    {
        private readonly HrmssisContext _context;

        public VacacionesAusenciasEmpleadoController(HrmssisContext context)
        {
            _context = context;
        }

        // GET: VacacionesAusenciasEmpleado
        public async Task<IActionResult> Index()
        {
              return _context.VacacionesAusencias != null ? 
                          View(await _context.VacacionesAusencias.ToListAsync()) :
                          Problem("Entity set 'HrmssisContext.VacacionesAusencias'  is null.");
        }

        // GET: VacacionesAusenciasEmpleado/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.VacacionesAusencias == null)
            {
                return NotFound();
            }

            var vacacionesAusencia = await _context.VacacionesAusencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacacionesAusencia == null)
            {
                return NotFound();
            }

            return View(vacacionesAusencia);
        }

        // GET: VacacionesAusenciasEmpleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VacacionesAusenciasEmpleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inicio,Finalizacion,Motivo,Estado")] VacacionesAusencia vacacionesAusencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacacionesAusencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacacionesAusencia);
        }

        // GET: VacacionesAusenciasEmpleado/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.VacacionesAusencias == null)
            {
                return NotFound();
            }

            var vacacionesAusencia = await _context.VacacionesAusencias.FindAsync(id);
            if (vacacionesAusencia == null)
            {
                return NotFound();
            }
            return View(vacacionesAusencia);
        }

        // POST: VacacionesAusenciasEmpleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Inicio,Finalizacion,Motivo,Estado")] VacacionesAusencia vacacionesAusencia)
        {
            if (id != vacacionesAusencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacacionesAusencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacacionesAusenciaExists(vacacionesAusencia.Id))
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
            return View(vacacionesAusencia);
        }

        // GET: VacacionesAusenciasEmpleado/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.VacacionesAusencias == null)
            {
                return NotFound();
            }

            var vacacionesAusencia = await _context.VacacionesAusencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacacionesAusencia == null)
            {
                return NotFound();
            }

            return View(vacacionesAusencia);
        }

        // POST: VacacionesAusenciasEmpleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.VacacionesAusencias == null)
            {
                return Problem("Entity set 'HrmssisContext.VacacionesAusencias'  is null.");
            }
            var vacacionesAusencia = await _context.VacacionesAusencias.FindAsync(id);
            if (vacacionesAusencia != null)
            {
                _context.VacacionesAusencias.Remove(vacacionesAusencia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacacionesAusenciaExists(long id)
        {
          return (_context.VacacionesAusencias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

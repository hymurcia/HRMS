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
    public class EmpleadoController : Controller
    {
        private readonly HrmssisContext _context;

        public EmpleadoController(HrmssisContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            var hrmssisContext = _context.Empleados.Include(e => e.IdDepartamentoNavigation).Include(e => e.IdRolNavigation);
            return View(await hrmssisContext.ToListAsync());
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento");
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdMaster", "IdMaster");
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Telefono,Direccion,Correo,IdDepartamento,FechaContratacion,IdRol")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento", empleado.IdDepartamento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdMaster", "IdMaster", empleado.IdRol);
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento", empleado.IdDepartamento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdMaster", "IdMaster", empleado.IdRol);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Cedula,Nombre,Telefono,Direccion,Correo,IdDepartamento,FechaContratacion,IdRol")] Empleado empleado)
        {
            if (id != empleado.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Cedula))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "IdDepartamento", "IdDepartamento", empleado.IdDepartamento);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdMaster", "IdMaster", empleado.IdRol);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'HrmssisContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(long id)
        {
          return (_context.Empleados?.Any(e => e.Cedula == id)).GetValueOrDefault();
        }
    }
}

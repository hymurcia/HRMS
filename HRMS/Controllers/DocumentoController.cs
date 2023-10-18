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
    public class DocumentoController : Controller
    {
        private readonly HrmssisContext _context;

        public DocumentoController(HrmssisContext context)
        {
            _context = context;
        }

        // GET: Documento
        public async Task<IActionResult> Index()
        {
            var hrmssisContext = _context.Documentos.Include(d => d.IdCedulaNavigation);
            return View(await hrmssisContext.ToListAsync());
        }

        // GET: Documento/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Documentos == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos
                .Include(d => d.IdCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdCedula == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // GET: Documento/Create
        public IActionResult Create()
        {
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula");
            return View();
        }

        // POST: Documento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCedula,Cedula,Contrato,Otros")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula", documento.IdCedula);
            return View(documento);
        }

        // GET: Documento/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Documentos == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula", documento.IdCedula);
            return View(documento);
        }

        // POST: Documento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCedula,Cedula,Contrato,Otros")] Documento documento)
        {
            if (id != documento.IdCedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoExists(documento.IdCedula))
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
            ViewData["IdCedula"] = new SelectList(_context.Empleados, "Cedula", "Cedula", documento.IdCedula);
            return View(documento);
        }

        // GET: Documento/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Documentos == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos
                .Include(d => d.IdCedulaNavigation)
                .FirstOrDefaultAsync(m => m.IdCedula == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // POST: Documento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Documentos == null)
            {
                return Problem("Entity set 'HrmssisContext.Documentos'  is null.");
            }
            var documento = await _context.Documentos.FindAsync(id);
            if (documento != null)
            {
                _context.Documentos.Remove(documento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentoExists(long id)
        {
          return (_context.Documentos?.Any(e => e.IdCedula == id)).GetValueOrDefault();
        }
    }
}

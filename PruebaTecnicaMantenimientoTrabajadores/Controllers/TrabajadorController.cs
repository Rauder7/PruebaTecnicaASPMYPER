using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMantenimientoTrabajadores.Context;
using PruebaTecnicaMantenimientoTrabajadores.Models.Dtos;
using PruebaTecnicaMantenimientoTrabajadores.Models.Entities;

namespace PruebaTecnicaMantenimientoTrabajadores.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly AppDbContext _context;

        public TrabajadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetProvincias(int idDepartamento)
        {
            var provincias = _context.Provincia
                .Where(p => p.IdDepartamento == idDepartamento)
                .Select(p => new { p.Id, p.NombreProvincia })
                .ToList();
            return Json(provincias);
        }

        [HttpGet]
        public JsonResult GetDistritos(int idProvincia)
        {
            var distritos = _context.Distritos
                .Where(d => d.IdProvincia == idProvincia)
                .Select(d => new { d.Id, d.NombreDistrito })
                .ToList();
            return Json(distritos);
        }


        // GET: Trabajador

        public async Task<IActionResult> Index(string sexo)
        {
            ViewBag.IdDepartamento = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            var data = await _context.Set<TrabajadorDto>().FromSqlRaw("EXEC sp_ListarTrabajadores").ToListAsync();
            if (!string.IsNullOrEmpty(sexo))
            {
                data = data.Where(t => t.Sexo == sexo).ToList();
            }
            ViewBag.FiltroSexo = sexo;
            return View(data);

        }

        // POST: Trabajador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error", "Home");

        }

        // POST: Trabajador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (id != trabajador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorExists(trabajador.Id))
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
            return RedirectToAction("Error", "Home");
        }

        // POST: Trabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadorExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }
    }
}

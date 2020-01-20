using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionNetCore.Models;

namespace AplicacionNetCore.Controllers
{
    public class ContactoesController : Controller
    {
        private readonly AplicacionNetCoreContext _context;

        public ContactoesController(AplicacionNetCoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NombreSort"] = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewData["DireccionSort"] = sortOrder == "direccion_asc" ? "direccion_desc" : "direccion_asc";
            ViewData["CurrentFilter"] = searchString;

            var contactos = from s in _context.Contacto select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                contactos = contactos.Where(s => s.Nombre.Contains(searchString) || s.Direccion.Contains(searchString) || s.Telefono.Contains(searchString) || s.Email.Contains(searchString));
            }

            switch (sortOrder) {
                case "nombre_desc": contactos = contactos.OrderByDescending(s => s.Nombre); break;
                case "direccion_desc": contactos = contactos.OrderByDescending(s => s.Direccion); break;
                case "direccion_asc": contactos = contactos.OrderBy(s => s.Direccion); break;
                default: contactos = contactos.OrderBy(s => s.ContactoID); break;
            }

            //return View(await _context.Contacto.ToListAsync());
            return View(await contactos.AsNoTracking().ToListAsync());
        }

        // GET: Contactoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto
                .FirstOrDefaultAsync(m => m.ContactoID == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // GET: Contactoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactoID,Nombre,Telefono,Direccion,Email,Verificado")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactoID,Nombre,Telefono,Direccion,Email,Verificado")] Contacto contacto)
        {
            if (id != contacto.ContactoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.ContactoID))
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
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contacto
                .FirstOrDefaultAsync(m => m.ContactoID == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacto = await _context.Contacto.FindAsync(id);
            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id)
        {
            return _context.Contacto.Any(e => e.ContactoID == id);
        }
    }
}

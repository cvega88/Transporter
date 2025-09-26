using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transporter.Core;
using Transporter.Data;

namespace Transporter.web.Controllers
{
    [Authorize]
    public class TransportersController : Controller
    {
        private readonly AppDbContext _context;

        public TransportersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Transporters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transporters.ToListAsync());
        }

        // GET: Transporters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transporter = await _context.Transporters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transporter == null)
            {
                return NotFound();
            }

            return View(transporter);
        }

        // GET: Transporters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transporters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Document,Phone,Email")] TransporterClass transporter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transporter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transporter);
        }

        // GET: Transporters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transporter = await _context.Transporters.FindAsync(id);
            if (transporter == null)
            {
                return NotFound();
            }
            return View(transporter);
        }

        // POST: Transporters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Document,Phone,Email")] TransporterClass transporter)
        {
            if (id != transporter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transporter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransporterExists(transporter.Id))
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
            return View(transporter);
        }

        // GET: Transporters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transporter = await _context.Transporters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transporter == null)
            {
                return NotFound();
            }

            return View(transporter);
        }

        // POST: Transporters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transporter = await _context.Transporters.FindAsync(id);
            if (transporter != null)
            {
                _context.Transporters.Remove(transporter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransporterExists(string id)
        {
            return _context.Transporters.Any(e => e.Id == id);
        }
    }
}

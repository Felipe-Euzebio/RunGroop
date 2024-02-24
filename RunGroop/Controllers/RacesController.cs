using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunGroop.Data;
using RunGroop.Models;

namespace RunGroop.Controllers
{
    public class RacesController : Controller
    {
        private readonly AppDbContext _context;

        public RacesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Races
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Races.Include(r => r.Address).Include(r => r.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Races/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.Address)
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // GET: Races/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id");
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            return View();
        }

        // POST: Races/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Image,StartTime,EntryFee,Website,Twitter,Facebook,Contact,AddressId,RaceCategory,AppUserId")] Race race)
        {
            if (ModelState.IsValid)
            {
                _context.Add(race);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", race.AddressId);
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", race.AppUserId);
            return View(race);
        }

        // GET: Races/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", race.AddressId);
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", race.AppUserId);
            return View(race);
        }

        // POST: Races/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image,StartTime,EntryFee,Website,Twitter,Facebook,Contact,AddressId,RaceCategory,AppUserId")] Race race)
        {
            if (id != race.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(race);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceExists(race.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", race.AddressId);
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", race.AppUserId);
            return View(race);
        }

        // GET: Races/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.Address)
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Races == null)
            {
                return Problem("Entity set 'AppDbContext.Races'  is null.");
            }
            var race = await _context.Races.FindAsync(id);
            if (race != null)
            {
                _context.Races.Remove(race);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceExists(int id)
        {
          return _context.Races.Any(e => e.Id == id);
        }
    }
}

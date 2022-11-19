using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;

namespace ChessPlayersRatingApp.Controllers
{
    public class PlayersController : Controller
    {
        private readonly AppDbContext _db;

        public PlayersController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Players
        // SORT
        public async Task<IActionResult> Index(SortState sortOrder = SortState.RankAsc)
        {
            IQueryable<Player> players = _db.Players.AsQueryable();

            ViewData["RankSort"] = sortOrder == SortState.RankAsc ? SortState.RankDesc : SortState.RankAsc;
            ViewData["RatingSort"] = sortOrder == SortState.RatingAsc ? SortState.RatingDesc : SortState.RatingAsc;
            ViewData["GameSort"] = sortOrder == SortState.GamesAsc ? SortState.GamesAsc : SortState.GamesAsc;
            ViewData["BirthYearSort"] = sortOrder == SortState.BirthYearyAsc ? SortState.BirthYearDesc : SortState.BirthYearyAsc;
            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["CountrySort"]= sortOrder == SortState.CountryAsc ? SortState.CountryDesc : SortState.CountryAsc;


            players = sortOrder switch
            {
                SortState.RankDesc => players.OrderByDescending(s => s.Rank),
                SortState.RatingAsc => players.OrderBy(s => s.Rating),
                SortState.NameAsc => players.OrderBy(s => s.Name),
                SortState.CountryDesc => players.OrderBy(s => s.Country),
                SortState.GamesAsc => players.OrderByDescending(s => s.Games),
                SortState.BirthYearyAsc => players.OrderByDescending(s => s.BirthYear),
                SortState.TitleAsc => players.OrderByDescending(s => s.Title),
                _ => players.OrderBy(s => s.Rank),
            };
            return View(await players.AsNoTracking().ToListAsync());
            //GET
            //return View(await _context.Players.ToListAsync());
        }
       
        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _db.Players
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rank,Name,Title,Country,Rating,Games,BirthYear")] Player player)
        {
            if (ModelState.IsValid)
            {
                _db.Add(player);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rank,Name,Title,Country,Rating,Games,BirthYear")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(player);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _db.Players
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _db.Players.FindAsync(id);
            _db.Players.Remove(player);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _db.Players.Any(e => e.Id == id);
        }
    }
}

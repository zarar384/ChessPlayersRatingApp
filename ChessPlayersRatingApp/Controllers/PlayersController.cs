using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using AutoMapper;
using ChessPlayersRatingApp.Models.DTO;

namespace ChessPlayersRatingApp.Controllers
{
    public class PlayersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public PlayersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: Players
        // SORT
        public async Task<IActionResult> Index(SortState sortOrder = SortState.RankAsc)
        {
            IQueryable<Player> players = _db.Players.AsQueryable();
            IQueryable<PlayerDto> playersMapper = _mapper.ProjectTo<PlayerDto>(players); 

            ViewData["RankSort"] = sortOrder == SortState.RankAsc ? SortState.RankDesc : SortState.RankAsc;
            ViewData["RatingSort"] = sortOrder == SortState.RatingAsc ? SortState.RatingDesc : SortState.RatingAsc;
            ViewData["GameSort"] = sortOrder == SortState.GamesAsc ? SortState.GamesAsc : SortState.GamesAsc;
            ViewData["BirthYearSort"] = sortOrder == SortState.BirthYearyAsc ? SortState.BirthYearDesc : SortState.BirthYearyAsc;
            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["CountrySort"]= sortOrder == SortState.CountryAsc ? SortState.CountryDesc : SortState.CountryAsc;


            playersMapper = sortOrder switch
            {
                SortState.RankDesc => playersMapper.OrderByDescending(s => s.Rank),
                SortState.RatingAsc => playersMapper.OrderBy(s => s.Rating),
                SortState.NameAsc => playersMapper.OrderBy(s => s.Name),
                SortState.CountryDesc => playersMapper.OrderBy(s => s.Country),
                SortState.GamesAsc => playersMapper.OrderByDescending(s => s.Games),
                SortState.BirthYearyAsc => playersMapper.OrderByDescending(s => s.BirthYear),
                SortState.TitleAsc => playersMapper.OrderByDescending(s => s.Title),
                _ => playersMapper.OrderBy(s => s.Rank),
            };
            return View(await playersMapper.AsNoTracking().ToListAsync());
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
            if(_db.Information.Any(m => m.PlayerId == id)) { 
                var info = await _db.Information.Where(m => m.PlayerId == id).FirstOrDefaultAsync();
                _db.Information.Remove(info);
            }
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

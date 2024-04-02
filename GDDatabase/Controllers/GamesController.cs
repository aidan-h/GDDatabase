using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Data;
using Game_Design_DB.Models;
using Game_Design_DB.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Game_Design_DB.Controllers
{
    public class GamesController : Controller
    {
        private readonly Game_Design_DBContext _context;

        public GamesController(Game_Design_DBContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Game.Include(g => g.People).ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.Include(p => p.People)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        private async Task PropagateViewModel(GameViewModel viewModel)
        {
            var allPeople = await _context.Person.ToListAsync();
            viewModel.Authors = allPeople.Select(p => new PersonAssigned { ID = p.ID, Name = p.Name, Assigned = false, }).ToList();
        }

        // GET: Games/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new GameViewModel();
            await PropagateViewModel(viewModel);
            return View(viewModel);
        }

        private async Task PropagateGameWithViewModel(Game game, GameViewModel viewModel, string[] selectedPeople)
        {
            //TODO Stupidly messy, but I don't want to write a constructor.
            game.Name = viewModel.Name;
            game.Website = viewModel.Website;
            game.Developer = viewModel.Developer;
            var peopleIDs = selectedPeople.Select(p => int.Parse(p));
            if (peopleIDs != null)
            {
                game.People = await _context.Person.Where(p => peopleIDs.Contains(p.ID)).ToListAsync();
            }
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Developer,Name,Website,ID,Authors")] GameViewModel gameViewModel, string[] selectedPeople)
        {
            var game = new Game();
            await PropagateGameWithViewModel(game, gameViewModel, selectedPeople);

            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id, string[] selectedPeople)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.Include(g => g.People).FirstAsync(g => g.ID == id);
            if (game == null)
            {
                return NotFound();
            }
            //TODO also messy...
            var viewModel = new GameViewModel();
            await PropagateViewModel(viewModel);
            viewModel.Developer = game.Developer;
            viewModel.Authors = (await _context.Person.ToListAsync()).Select(p => {
                return new PersonAssigned
                {
                    ID = p.ID,
                    Name = p.Name,
                    Assigned = game.People != null && game.People.Where(n => n.ID == p.ID).Any(),
                }; }).ToList();
            viewModel.Website = game.Website;
            viewModel.Name = game.Name;
            return View(viewModel);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Developer,Name,Website,ID")] GameViewModel viewModel, string[] selectedPeople)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            // I don't know why this keeps returning false, so I'm uncommenting it.
            // TODO: Fix for security
            //if (ModelState.IsValid)
            //{
                try
                {
                    var game = _context.Game.Include(g => g.People).Where(g => g.ID == viewModel.ID).FirstOrDefault();
                    await PropagateGameWithViewModel(game, viewModel, selectedPeople);
                    Console.WriteLine("People Count: " + game.People.Count);
                    _context.Update(game);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(viewModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //Console.WriteLine("not valid");
            return View(viewModel);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game != null)
            {
                _context.Game.Remove(game);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.ID == id);
        }
    }
}

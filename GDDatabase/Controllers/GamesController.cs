using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Data;
using Game_Design_DB.Helpers;
using Game_Design_DB.Models;
using Game_Design_DB.ViewModels;

namespace Game_Design_DB.Controllers
{
    public class GamesController : DBSetController<Game, GameViewModel>
    {
        public GamesController(Game_Design_DBContext c)
        {
            context = c;
            dbSet = c.Game;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Developer,Name,Website,ID,SelectedPeople")] GameViewModel viewModel, string[] selectedPeople) => await ApplyEdit(id, viewModel);

        public override IQueryable<Game> IncludeDBSet() => dbSet.Include(g => g.People);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Developer,Name,Website,ID,Authors,SelectedPeople")] GameViewModel gameViewModel) => await ApplyCreate(gameViewModel);

        public async override Task<GameViewModel> PropogateViewModel(Game model) => new GameViewModel
        {
            Developer = model.Developer,
            Authors = await AssignedSet.Fetch(context.Person, model.People),
            Website = model.Website,
            Name = model.Name,
        };
    }
}

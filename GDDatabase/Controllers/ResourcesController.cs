using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Data;
using Game_Design_DB.Models;
using Game_Design_DB.ViewModels;
using Game_Design_DB.Helpers;

namespace Game_Design_DB.Controllers
{
    public class ResourcesController : DBSetController<Resource, ResourceViewModel>
    {
        public ResourcesController(Game_Design_DBContext c)
        {
            context = c;
            dbSet = c.Resource;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,URL,ID,Authors,Games")] ResourceViewModel viewModel, CheckBoxItem[] selectedPeople, CheckBoxItem[] selectedGames) {
            viewModel.Authors = await AssignedSet.FromCheckBoxItems(context.Person, selectedPeople);
            viewModel.Games = await AssignedSet.FromCheckBoxItems(context.Person, selectedGames);
            return await ApplyEdit(id, viewModel);
        }

        public override IQueryable<Resource> IncludeDBSet() => dbSet.Include(g => g.Games).Include(g => g.Authors);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,URL,ID,Authors,Games")] ResourceViewModel viewModel, CheckBoxItem[] selectedPeople, CheckBoxItem[] selectedGames) {
            viewModel.Authors = await AssignedSet.FromCheckBoxItems(context.Person, selectedPeople);
            viewModel.Games = await AssignedSet.FromCheckBoxItems(context.Person, selectedGames);
            return await ApplyCreate(viewModel);
        }

        public async override Task<ResourceViewModel> PropogateViewModel(Resource model) => new ResourceViewModel
        {
            Title = model.Title,
            Authors = await AssignedSet.Fetch(context.Person, model.Authors),
            Games = await AssignedSet.Fetch(context.Game, model.Games),
            URL = model.URL,
        };
    }
}

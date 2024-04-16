using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Data;
using Game_Design_DB.Models;
using Game_Design_DB.ViewModels;
using Game_Design_DB.Helpers;

namespace Game_Design_DB.Controllers
{
    public class PeopleController : DBSetController<Person, PersonViewModel>
    {
        public PeopleController(Game_Design_DBContext c)
        {
            context = c;
            dbSet = c.Person;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID,Games,Websites,Resources")] PersonViewModel viewModel, CheckBoxItem[] selectedGames, CheckBoxItem[] selectedResources, CheckBoxItem[] selectedWebsites) {
            viewModel.Games = await AssignedSet.FromCheckBoxItems(context.Person, selectedGames);
            viewModel.Resources = await AssignedSet.FromCheckBoxItems(context.Person, selectedResources);
            viewModel.Websites = await AssignedSet.FromCheckBoxItems(context.Person, selectedWebsites);
            return await ApplyEdit(id, viewModel);
        }

        public override IQueryable<Person> IncludeDBSet() => dbSet.Include(g => g.Games).Include(g => g.Websites).Include(g => g.Resources);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Name,ID,Games,Websites,Resources")] PersonViewModel viewModel, CheckBoxItem[] selectedGames, CheckBoxItem[] selectedResources, CheckBoxItem[] selectedWebsites) {
            viewModel.Games = await AssignedSet.FromCheckBoxItems(context.Person, selectedGames);
            viewModel.Resources = await AssignedSet.FromCheckBoxItems(context.Person, selectedResources);
            viewModel.Websites = await AssignedSet.FromCheckBoxItems(context.Person, selectedWebsites);
            return await ApplyCreate(viewModel);
        }

        public async override Task<PersonViewModel> PropogateViewModel(Person model) => new PersonViewModel
        {
            Name = model.Name,
            Games = await AssignedSet.Fetch(context.Game, model.Games),
            Resources = await AssignedSet.Fetch(context.Resource, model.Resources),
            Websites = await AssignedSet.Fetch(context.PersonalWebsite, model.Websites),
        };
    }
}

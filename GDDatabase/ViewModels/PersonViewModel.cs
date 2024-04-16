using Game_Design_DB.Helpers;
using Game_Design_DB.Models;
using Game_Design_DB.Data;
using Microsoft.EntityFrameworkCore;

namespace Game_Design_DB.ViewModels
{
    public class PersonViewModel : IDBSetViewModel<Person, PersonViewModel> {
        public async Task PropagateModel(Person model, Game_Design_DBContext context)
        {
            model.Name = Name;
            var gameIds = Games.SelectedIDs();
            model.Games = await context.Game.Where(p => gameIds.Contains(p.ID)).ToListAsync();
            var resourceIds = Resources.SelectedIDs();
            model.Resources = await context.Resource.Where(p => resourceIds.Contains(p.ID)).ToListAsync();
            var websiteIds = Websites.SelectedIDs();
            model.Websites = await context.PersonalWebsite.Where(p => websiteIds.Contains(p.ID)).ToListAsync();
        }
        public static async Task<PersonViewModel> FromContext(Game_Design_DBContext context) => new PersonViewModel 
        {
            Games = await AssignedSet.Fetch(context.Game),
            Resources = await AssignedSet.Fetch(context.Resource),
            Websites = await AssignedSet.Fetch(context.PersonalWebsite)
        };

        public async static Task<PersonViewModel> FromModel(Game_Design_DBContext context, Person model) => new PersonViewModel
        {
            Name = model.Name,
            Games = new AssignedSet
            {
                Objects = (await context.Game.ToListAsync()).Select(p =>
                {
                    return new AssignedObject
                    {
                        ID = p.ID,
                        Name = p.Name,
                        Assigned = model.Games != null && model.Games.Where(n => n.ID == p.ID).Any(),
                    };
                }).ToList()
            },
        };


        public string Name
        {
            get; set;
        }
        public int ID
        {
            get; set;
        }


        public AssignedSet Games { get; set; }
        public AssignedSet Resources { get; set; }
        public AssignedSet Websites { get; set; }
    }
}

using Game_Design_DB.Helpers;
using Game_Design_DB.Models;
using Game_Design_DB.Data;
using Microsoft.EntityFrameworkCore;

namespace Game_Design_DB.ViewModels
{
    public class ResourceViewModel : IDBSetViewModel<Resource, ResourceViewModel> {
        public async Task PropagateModel(Resource model, Game_Design_DBContext context)
        {
            model.Title = Title;
            model.URL = URL;
            var authorIds = Authors.SelectedIDs();
            model.Authors = await context.Person.Where(p => authorIds.Contains(p.ID)).ToListAsync();
            var gameIds = Games.SelectedIDs();
            model.Games = await context.Game.Where(p => gameIds.Contains(p.ID)).ToListAsync();
        }

        public async static Task<ResourceViewModel> FromModel(Game_Design_DBContext context, Resource model) => new ResourceViewModel
        {
            Title= model.Title,
            URL=model.URL,
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
            Authors = new AssignedSet
            {
                Objects = (await context.Person.ToListAsync()).Select(p =>
                {
                    return new AssignedObject
                    {
                        ID = p.ID,
                        Name = p.Name,
                        Assigned = model.Authors != null && model.Authors.Where(n => n.ID == p.ID).Any(),
                    };
                }).ToList()
            },
        };

        public static async Task<ResourceViewModel> FromContext(Game_Design_DBContext context) => new ResourceViewModel 
        {
            Authors = await AssignedSet.Fetch(context.Person),
            Games = await AssignedSet.Fetch(context.Game)
        };

        public String Title { get; set; }
        public String URL { get; set; }
        public int ID {  get; set; }
        public AssignedSet Authors { get; set; } 
        public AssignedSet Games { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Game_Design_DB.Helpers;
using Game_Design_DB.Models;
using Game_Design_DB.Data;
using Microsoft.EntityFrameworkCore;

namespace Game_Design_DB.ViewModels
{
    public class GameViewModel : IDBSetViewModel<Game, GameViewModel>
    {
        public async Task PropagateModel(Game model, Game_Design_DBContext context)
        {
            model.Name = Name;
            model.Website = Website;
            model.Developer = Developer;
            var Ids = Authors.SelectedIDs();
            model.People = await context.Person.Where(p => Ids.Contains(p.ID)).ToListAsync();
        }

        public async static Task<GameViewModel> FromModel(Game_Design_DBContext context, Game model) => new GameViewModel
        {
            Developer = model.Developer,
            Authors = new AssignedSet
            {
                Objects = (await context.Person.ToListAsync()).Select(p =>
                {
                    return new AssignedObject
                    {
                        ID = p.ID,
                        Name = p.Name,
                        Assigned = model.People != null && model.People.Where(n => n.ID == p.ID).Any(),
                    };
                }).ToList()
            },
            Website = model.Website,
            Name = model.Name,
        };

        public static async Task<GameViewModel> FromContext(Game_Design_DBContext context) => new GameViewModel
        {
            Authors = await AssignedSet.Fetch(context.Person)
        };

        public int ID { get; set; }
        public String Developer { get; set; }
        public String Name { get; set; }
        [Url]
        public String? Website { get; set; }
        public AssignedSet Authors { get; set; }
    }
}

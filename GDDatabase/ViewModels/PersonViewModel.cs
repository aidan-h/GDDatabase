using Game_Design_DB.Data;
using Game_Design_DB.Helpers;
using Game_Design_DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Game_Design_DB.ViewModels
{
    public class PersonViewModel
    {

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
        public static async Task<PersonViewModel> FromModel(Person model, Game_Design_DBContext context)
        {
            return new PersonViewModel
            {
                Name = model.Name,
                Games = await AssignedSet.Fetch(context.Game),
                Resources = await AssignedSet.Fetch(context.Resource),
                Websites = await AssignedSet.Fetch(context.PersonalWebsite),
            };
        }
    }
}

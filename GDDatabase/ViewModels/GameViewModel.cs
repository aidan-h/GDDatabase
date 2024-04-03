using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Game_Design_DB.Models;

namespace Game_Design_DB.ViewModels
{
    public class GameViewModel
    {
        public int ID { get; set; }
        public String Developer { get; set; }
        public String Name { get; set; }
        [Url]
        public String? Website { get; set; }
        public ICollection<PersonAssigned> Authors { get; set; }
        public IEnumerable<int> SelectedPeople { get; set; } = new List<int>();
    }

    public abstract class AssignedObject
    {
        public int ID { get; set; }
        public bool Assigned { get; set; }
        public abstract string DisplayName();
    }

    public class PersonAssigned : AssignedObject
    {
        public string Name { get; set; }
        public override string DisplayName() => Name;
    }
}

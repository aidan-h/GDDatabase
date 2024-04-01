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
    }

    public class PersonAssigned
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}

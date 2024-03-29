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
        [Display(Name ="Authors")]
        public IEnumerable<string> PeopleIDs { get; set;  } = new List<string>();
        public List<SelectListItem> People { get; set; }
    }
}

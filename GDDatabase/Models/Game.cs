using Game_Design_DB.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Game : IAssignedObject<Game>
    {
        public static Game Default() => new Game();
        [Required]
        public String Developer { get; set; }
        [Required]
        public String Name { get; set; }
        [Url]
        public String? Website { get; set; }
        [Key]
        public int ID {  get; set; }
        public ICollection<Person> People { get; set;  } = new List<Person>();
        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
    }
}

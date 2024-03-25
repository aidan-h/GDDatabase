using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Game
    {
        [Required]
        public Developer Developer { get; set; }
        [Required]
        public String Name { get; set; }
        [Url]
        public String? Website { get; set; }
        [Key]
        public int ID {  get; set; }
        public List<Person> People { get; } = new();
    }
}

using Game_Design_DB.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Developer
    {
        [Required]
        public string Name { get; set; }
        [Key]
        public int ID { get; set; }

        public ICollection<Engine> Engines { get; set; } = new List<Engine>();
        public ICollection<Person> People { get; set; } = new List<Person>();
    }
}

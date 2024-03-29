using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Person
    {
        [Required]
        public string Name
        {
            get; set;
        }
        [Key]
        public int ID
        {
            get; set;
        }

        public ICollection<Game> Games { get; } = new List<Game>();
        public ICollection<Resource> Resources { get; set; } = new List<Resource>();

        public ICollection<PersonalWebsite> Websites { get; set; } = new List<PersonalWebsite>();
    } 
}

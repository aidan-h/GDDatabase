using Game_Design_DB.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Person : IAssignedObject<Person>
    {
        public static Person Default() => new Person();
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


        public ICollection<Game> Games { get; set; } = new List<Game>();
        public ICollection<Resource> Resources { get; set; } = new List<Resource>();

        public ICollection<PersonalWebsite> Websites { get; set; } = new List<PersonalWebsite>();
    } 
}

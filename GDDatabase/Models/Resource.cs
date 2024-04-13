using Game_Design_DB.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Resource : IAssignedObject<Resource>
    {
        public static Resource Default() => new Resource();
        [Required]
        public String Title { get; set; }
        [Url]
        [Required]
        public String URL { get; set; }
        [Key]
        public int ID {  get; set; }
        public String Name => Title;
        public ICollection<Person> Authors { get; set; } = new List<Person>();
        public ICollection<Game> Games { get; set; } = new List<Game>();

    }
}

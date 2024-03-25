using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Podcast
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public List<Person> Speakers { get; } = new();

        [Url]
        [Required]
        public string URL { get; set; }
    }
}

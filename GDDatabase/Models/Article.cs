using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Url]
        public string Website { get; set; }
        public List<Person> Authors { get; } = new();
    }
}

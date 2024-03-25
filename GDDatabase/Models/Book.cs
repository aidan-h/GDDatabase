using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public List<Person> Authors { get; } = new();

        [Url]
        public string? Website { get; set; } 
        public int? ISBN {  get; set; }
    }
}

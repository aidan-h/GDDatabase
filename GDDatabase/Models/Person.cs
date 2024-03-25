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

        public List<Game> Games { get; } = new();
        public List<Book> Books { get; } = new();
        public List<Podcast> Podcasts { get; } = new();
        public List<Article> Articles { get; } = new();

        [Url]
        public string? Website { get; set; }
    } 
}

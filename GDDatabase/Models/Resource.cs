using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Resource 
    {
        [Required]
        public String Title { get; set; }
        [Url]
        [Required]
        public String URL { get; set; }
        [Key]
        public int ID {  get; set; }
        public ICollection<Person> Authors { get; set; } = new List<Person>();
        [Display(Name = "Related Games")]
        public ICollection<Game> RelatedGames { get; set; } = new List<Game>();

    }
}

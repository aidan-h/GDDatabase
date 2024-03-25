using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Video
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public List<Person> Speakers { get; }

        [Required]
        [Url]
        public string URL { get; set; }
    }
}

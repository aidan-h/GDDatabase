using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Developer
    {
        [Required]
        public string Name { get; set; }
        [Key]
        public int ID { get; set; }
    }
}

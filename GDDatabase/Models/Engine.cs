using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class Engine
    {
        [Required]
        public string Name { get; set; }
        [Key]
        public int ID { get; set; }
        [Url]
        public string? Website { get; set; }
        public Developer? Developer { get; set; } = null!;

    }
}

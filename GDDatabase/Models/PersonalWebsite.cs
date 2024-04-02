using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class PersonalWebsite
    {
        [Key]
        public int ID { get; set; }

        [Url]
        public string URL { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}

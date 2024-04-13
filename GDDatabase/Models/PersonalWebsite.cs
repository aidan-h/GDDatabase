using Game_Design_DB.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Game_Design_DB.Models
{
    public class PersonalWebsite : IAssignedObject<PersonalWebsite>
    {
        public static PersonalWebsite Default() => new PersonalWebsite();

        [Key]
        public int ID { get; set; }

        [Url]
        public string URL { get; set; }
        public string Name => URL;
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}

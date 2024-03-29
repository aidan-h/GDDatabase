using Game_Design_DB.Models;
namespace Game_Design_DB.ViewModels
{
    public class AssignedAuthor {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public bool Assigned {  get; set; }
    }
}

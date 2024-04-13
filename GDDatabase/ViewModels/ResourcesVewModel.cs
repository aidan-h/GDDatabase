using Game_Design_DB.Models;
using Game_Design_DB.Helpers;

namespace Game_Design_DB.ViewModels
{
    public class ResourceViewModel {
        public String Title { get; set; }
        public String URL { get; set; }
        public int ID {  get; set; }
        public AssignedSet Authors { get; set; } 
        public AssignedSet Games { get; set; }
    }
}

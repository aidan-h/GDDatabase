using Microsoft.EntityFrameworkCore;

namespace Game_Design_DB.Helpers
{
    public class AssignedObject
    {
        public int ID { get; set; }
        public bool Assigned { get; set; } = false;
        public string Name { get; set; }
    }


    public interface IAssignedObject<S> where S : class
    {
        public static abstract S Default();

        public string Name { get; }
        public int ID { get; set; }
        AssignedObject AssignedObject(bool assigned) => new AssignedObject { Name = Name, ID = ID };
    }

    public class AssignedSet
    {
        public static async Task<AssignedSet> Fetch<S>(DbSet<S> dbSet, ICollection<S>? selected = null) where S : class, IAssignedObject<S>
        {
            var items = await dbSet.ToListAsync();
            return new AssignedSet
            {
                Objects = items.Select(p =>
                {
                    return new AssignedObject { Name = p.Name, ID = p.ID, Assigned = selected != null && selected.Where(n => n.ID == p.ID).Any() };
                }).ToList()
            };
        }

        public bool Contains(int id)
        {
            return SelectedObjects.Contains(id);
        }
        public ICollection<AssignedObject> Objects { get; set; }
        public IEnumerable<int> SelectedObjects { get; set; } = new List<int>();
    }
}

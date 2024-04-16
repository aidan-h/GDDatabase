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

    public class CheckBoxItem
    {
        public bool Checked { get; set; }
        public int ID { get; set; }
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
        public static async Task<AssignedSet> FetchIds<S>(DbSet<S> dbSet, IEnumerable<int>? selected = null) where S : class, IAssignedObject<S>
        {
            var items = await dbSet.ToListAsync();
            return new AssignedSet
            {
                Objects = items.Select(p =>
                {
                    return new AssignedObject { Name = p.Name, ID = p.ID, Assigned = selected != null && selected.Where(n => n == p.ID).Any() };
                }).ToList()
            };
        }

        public List<int> SelectedIDs()
        {
            var list = new List<int>();
            foreach (var obj in Objects)
                if (obj.Assigned)
                    list.Add(obj.ID);

            return list;

        }
        public bool Contains(int id)
        {
            foreach (var obj in Objects)
            {
                if (obj.ID == id)
                    return true;
            }
            return false;
        }
        public ICollection<AssignedObject> Objects { get; set; } = new List<AssignedObject>();
    }
}

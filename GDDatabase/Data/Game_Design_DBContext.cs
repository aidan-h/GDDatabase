using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Game_Design_DB.Models;

namespace Game_Design_DB.Data
{
    public class Game_Design_DBContext : DbContext
    {
        public Game_Design_DBContext (DbContextOptions<Game_Design_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Game_Design_DB.Models.Person> Person { get; set; } = default!;
        public DbSet<Game_Design_DB.Models.Game> Game { get; set; } = default!;
        public DbSet<Game_Design_DB.Models.Resource> Resource { get; set; } = default!;
        public DbSet<Game_Design_DB.Models.PersonalWebsite> PersonalWebsite { get; set; } = default!;
    }
}

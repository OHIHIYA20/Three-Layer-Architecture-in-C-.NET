using Models;
using System.Data.Entity;

namespace Data.Context
{
    public partial class TestEntities : DbContext
    {
        public TestEntities()
            : base("Server=(localdb)\\mssqllocaldb;Database=NewDb;Trusted_Connection=True;")
        {
        }

        public virtual DbSet<Role> role { get; set; }
        public virtual DbSet<User> user { get; set; }
        public virtual DbSet<Product> product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

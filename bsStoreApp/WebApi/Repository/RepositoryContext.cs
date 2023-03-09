using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repository.Config;

namespace WebApi.Repository
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions opt) : base(opt) { }
        public DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}

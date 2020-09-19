using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class TecheventContext : DbContext
    {
        public TecheventContext()
        {
        }

        public TecheventContext(DbContextOptions<TecheventContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
    }
}
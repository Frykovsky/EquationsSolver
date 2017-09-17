using Domain.UsersDB;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<EquationParameters> EquationParameters { get; set; }
        public DbSet<ExperimentsDB> ExperimentsDBs { get; set; }
    }
}

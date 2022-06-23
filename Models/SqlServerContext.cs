using Microsoft.EntityFrameworkCore;
using WebServer;

namespace InsuranceService.Infrastructure.Context
{
    public class SqlServerContext : DbContext
    {
        private readonly string _connectionString;

        public SqlServerContext()
        { }
        public SqlServerContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

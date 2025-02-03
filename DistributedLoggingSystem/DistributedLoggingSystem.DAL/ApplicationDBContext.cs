using DistributedLoggingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DistributedLoggingSystem.DAL
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration["LoggingConfig:Database:ConnectionString"];
                optionsBuilder.UseSqlServer(connectionString); // Use UseSqlServer() for SQL Server
            }
        }

        public virtual DbSet<Log> logs { get; set; }
    }
}

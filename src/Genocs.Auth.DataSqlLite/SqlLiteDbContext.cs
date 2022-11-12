namespace Genocs.Auth.DataSqlLite;

using Genocs.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class SqlLiteDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    private readonly IConfiguration Configuration;

    public SqlLiteDbContext()
    {
    }

    public SqlLiteDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlLiteDbContext).Assembly);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("AuthLiteDatabase"));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.DBContext;

public class Db : DbContext
{
    private readonly IConfiguration config;
    public DbSet<Users> Users {get; set;}= null!;

    public Db(DbContextOptions options, IConfiguration config) : base(options)
    {
        this.config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(config["ConnectionStrings:Dev"]);
        base.OnConfiguring(optionsBuilder);
    }
}
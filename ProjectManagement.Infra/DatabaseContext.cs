using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infra.Models;

namespace ProjectManagement.Infra;

public class DatabaseContext : DbContext
{
    public DbSet<CardModel> Cards { get; set; }
    public DbSet<PersonModel> People { get; set; }
    public DbSet<ListModel> Lists { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }
}

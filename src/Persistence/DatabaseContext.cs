using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Persistence;

public class DatabaseContext : DbContext
{

  public DatabaseContext
  (
    DbContextOptions<DatabaseContext> options
  ) : base(options)
  {

  }
  public DbSet<Pessoa> Pessoas { get; set; }
  public DbSet<Contrato> Contratos { get; set; }
  protected OnModelCreating(ModelBuilder builder)
  {

  }
}
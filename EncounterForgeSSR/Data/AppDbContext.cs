using EncounterForgeSSR.Models;
using Microsoft.EntityFrameworkCore;

namespace EncounterForgeSSR.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Encounter> Encounters => Set<Encounter>();

    public DbSet<MonsterEntry> MonsterEntries => Set<MonsterEntry>();

    public DbSet<MonsterCatalog> MonsterCatalogs => Set<MonsterCatalog>();

    public DbSet<AppUser> AppUsers => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Encounter>()
            .HasMany(e => e.MonsterEntries)
            .WithOne(m => m.Encounter)
            .HasForeignKey(m => m.EncounterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AppUser>()
            .HasIndex(user => user.UserName)
            .IsUnique();

    }
}
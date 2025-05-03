using Microsoft.EntityFrameworkCore;
using RiotStatsAPI.Models.Entities;

namespace RiotStatsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //RELATIONSHIPS
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Summoner)
                .WithOne(s => s.Account)
                .HasForeignKey<Summoner>(s => s.AccountId);
            //UNIQUE
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Puuid)
                .IsUnique();

            modelBuilder.Entity<Summoner>()
                .HasIndex(s => s.Puuid)
                .IsUnique();
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Summoner> Summoners { get; set; }
    }
}

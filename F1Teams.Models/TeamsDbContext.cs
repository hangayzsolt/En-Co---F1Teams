using F1Teams.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace F1Teams.Models
{
    public class TeamsDbContext : DbContext
    {
        public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("DataSource=:memory:");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.FoundationYear).IsRequired();
                entity.Property(e => e.WonChampionsTitle).IsRequired();
                entity.Property(e => e.IsEntryFeePayed).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

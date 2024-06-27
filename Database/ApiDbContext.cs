using api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<CardSet> CardSets { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CardSet>(entity =>
            {
                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date");  // Configurer le type de colonne comme date
            });
        }
    }
}

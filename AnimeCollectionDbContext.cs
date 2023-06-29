using System;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AnimeCollectionDbContext : DbContext
    {
        public AnimeCollectionDbContext() : base()
        {
        }

        public AnimeCollectionDbContext(DbContextOptions<AnimeCollectionDbContext> options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=AnimeCollectionDb;Username=postgres;Password=11");
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Studio> Studios { get; set; }
    }
}

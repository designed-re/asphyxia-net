using asphyxia.Models;
using Microsoft.EntityFrameworkCore;

namespace asphyxia
{
    public class AsphyxiaContext : DbContext
    {
        public AsphyxiaContext(DbContextOptions<AsphyxiaContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cardEntity = modelBuilder.Entity<Card>();
            cardEntity
                .HasIndex(card => card.CardId)
                .IsUnique();
            cardEntity
                .HasIndex(card => card.RefId)
                .IsUnique();
            cardEntity
                .HasIndex(card => card.DataId);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}

using ChessPlayersRatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessPlayersRatingApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Information>().OwnsOne(x => x.Image);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-7UKLE6TU; Database = ChessPlayers;Trusted_Connection=True; MultipleActiveResultSets= True;");
        }
        public DbSet<Player> Players{ get; set; }
        public DbSet<Information> Information { get; set; }
    }
}

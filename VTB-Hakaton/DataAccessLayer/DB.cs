using Microsoft.EntityFrameworkCore;
using System.Linq;
using VTB_Hakaton.DataAccessLayer.Models;
using WebApi.DataAccessLayer.Models;

namespace WebApi.DataAccessLayer
{
    public class DB : DbContext
    {
        public DB(DbContextOptions options) : base(options)
        {

        }
        // Добавить класс Для Нфт, с их копиями , хрвнить в бд факт передачи админу НФТ, потом выводить на фронет все НФТ пользователя
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivitySolution> ActivitySolutions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }

        //public IQueryable<User> Students => Users.Where(u => u.Role == UserRole.Worker);

        //public IQueryable<User> VisibleUsers => Users.Where(u => u.Role != UserRole.Superadmin);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Lead)
                .WithOne()
                .HasForeignKey<Group>(g => g.LeadId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ActivitySolution>()
                .HasOne(g => g.Author)
                .WithOne()
                .HasForeignKey<ActivitySolution>(g => g.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);
            /*
            modelBuilder.Entity<ActivitySolution>()
                .HasOne(x => x.Author)
                .WithMany(u => u.ActivitySolutions)
                .OnDelete(DeleteBehavior.SetNull);*/

            /*
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Leader)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);*/
            /*
            modelBuilder.Entity<GroupUserLink>()
                .HasOne<Group>(l => l.Group)
                .WithMany(g => g.GroupUserLinks)
                .HasForeignKey(l => l.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupUserLink>()
                .HasOne<User>(l => l.User)
                .WithMany(g => g.GroupUserLinks)
                .HasForeignKey(l => l.UserId);*/
        }
    }
}

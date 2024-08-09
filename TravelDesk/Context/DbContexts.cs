using Microsoft.EntityFrameworkCore;
using TravelDesk.Models;

namespace TravelDesk.Context
{
    public class DbContexts : DbContext
    {
        public DbContexts(DbContextOptions<DbContexts> options):base(
            options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().
                HasData(new Role
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                },
                  new Role
                  {
                      RoleId = 2,
                      RoleName = "HR TravelAdmin",
                      CreatedBy = 1,
                      CreatedOn = DateTime.Now,
                      IsActive = true
                  },
                   new Role
                   {
                       RoleId = 3,
                       RoleName = "Manager",
                       CreatedBy = 1,
                       CreatedOn = DateTime.Now,
                       IsActive = true
                   },
                   new Role
                   {
                       RoleId = 4,
                       RoleName = "Employee",
                       CreatedBy = 1,
                       CreatedOn = DateTime.Now,
                       IsActive = true
                   }
                   );

            modelBuilder.Entity<User>()
               .HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Manager)
                .WithMany()
                .HasForeignKey(u => u.ManagerId);

        }
    }
}

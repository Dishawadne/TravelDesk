using Microsoft.EntityFrameworkCore;
using TeavelDesk.Models;
using TravelDesk.Models;

namespace TravelDesk.Context
{
    public class DbContexts : DbContext
    {
        public DbContexts(DbContextOptions<DbContexts> options):base(
            options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AirBooking> AirBookings { get; set; }

        public DbSet<HotelBooking> HotelBookings { get; set; }
      

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



           
            modelBuilder.Entity<Employee>()
               .HasMany(e => e.HotelBookings)
               .WithOne(h => h.Employee)
               .HasForeignKey(h => h.EmployeeId);

            // Configure the one-to-many relationship between Employee and AirBooking
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AirBookings)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId);

            base.OnModelCreating(modelBuilder);


        }
    }
}

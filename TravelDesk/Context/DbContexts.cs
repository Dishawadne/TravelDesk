using Microsoft.EntityFrameworkCore;

using TravelDesk.Models;

namespace TravelDesk.Context
{
    public class DbContexts : DbContext
    {
        public DbContexts(DbContextOptions<DbContexts> options):base(
            options) { }
        //public DbContexts() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<Project> Projects { get; set; }
        public DbSet<CommonTypeRef> CommonTypes { get; set; }

       public DbSet<TravelRequest> TravelRequests { get; set; }
       
      

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


    //        modelBuilder.Entity<Project>().HasData(
    //new Project { ProjectId = 1, ProjectName = "Project Alpha", CreatedBy = 1, CreatedOn = DateTime.Now, IsActive = true },
    //new Project { ProjectId = 2, ProjectName = "Project Beta", CreatedBy = 1, CreatedOn = DateTime.Now, IsActive = true },
    //new Project { ProjectId = 3, ProjectName = "Project Gamma", CreatedBy = 1, CreatedOn = DateTime.Now, IsActive = true }
//);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Manager)
                .WithMany()
                .HasForeignKey(u => u.ManagerId);

            modelBuilder.Entity<User>()
         .HasOne(u => u.Role)
         .WithMany(r => r.Users)
         .HasForeignKey(u => u.RoleId)
         .OnDelete(DeleteBehavior.Restrict); // No cascade delete for Role


            modelBuilder.Entity<TravelRequest>()
       .HasKey(tr => tr.RequestId);


            modelBuilder.Entity<TravelRequest>()
           .HasOne(tr => tr.User)
           .WithMany(u => u.TravelRequests)
           .HasForeignKey(tr => tr.UserId)
           .OnDelete(DeleteBehavior.Restrict); // Optional: Configure the delete behavior

            // Define the relationship for Manager -> TravelRequest
            modelBuilder.Entity<TravelRequest>()
                .HasOne(tr => tr.Manager)
                .WithMany() // No inverse navigation property
                .HasForeignKey(tr => tr.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker.Entries<User>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = 1;
                    entity.CreatedOn = DateTime.Now;
                }

                entity.ModifiedBy = "1"; 
                entity.ModifiedOn = DateTime.Now;
            }
        }
    
}
}

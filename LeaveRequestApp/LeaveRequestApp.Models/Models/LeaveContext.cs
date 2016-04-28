using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveRequestApp.Models
{
    public class LeaveContext: IdentityDbContext<Employees>
    {
        public LeaveContext() : base("LeaveContext")
        {

        }

        static LeaveContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }

        public static LeaveContext Create()
        {
            return new LeaveContext();
        }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Years> Years { get; set; }
        public DbSet<Requests> Requests { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>().ToTable("IdentityUsers");
            modelBuilder.Entity<IdentityUserRole>().ToTable("IdentityUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("IdentityUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("IdentityUserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("IdentityRoles");
        }

    }
}
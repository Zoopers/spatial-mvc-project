namespace SpatialDimensionTest.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbContextCollection : DbContext
    {
        public DbContextCollection()
            : base("name=Employee")
        {
        }

        //Create DBContext class that will allow us access to object models
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<RegisteredUsers> RegisteredUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.EmpName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Job)
                .IsUnicode(false);


            modelBuilder.Entity<RegisteredUsers>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<RegisteredUsers>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}

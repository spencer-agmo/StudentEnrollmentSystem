using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.Data
{
    public class DataContext: IdentityDbContext<User, Role, Guid>

    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    
}
}

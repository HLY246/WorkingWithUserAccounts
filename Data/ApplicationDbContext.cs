using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WorkingWithUserAccounts.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDo>? ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ToDo>()
                .HasOne(todo => todo.User)
                .WithMany(user => user.ToDos)
                .HasForeignKey(todo => todo.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using ContentNegotiation.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentNegotiation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;Database=ContentNegotiation;Trusted_Connection=True;TrustServerCertificate=True;");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<Student>
            {
                new Student()
                {
                    Id =1,
                    Name ="Munib",
                    Address = "Peshawar, Pakistan"
                },
                new Student()
                {
                    Id =2,
                    Name ="Abdullah Khan",
                    Address = "Charsadda, Pakistan"
                },
                new Student()
                {
                    Id =3,
                    Name ="Shakeel",
                    Address = "Mianwali, Pakistan"
                },
                new Student()
                {
                    Id =4,
                    Name ="Seyab",
                    Address = "Malakand, Pakistan"
                }
            };

            builder.Entity<Student>().HasData(roles);
        }
    }
}


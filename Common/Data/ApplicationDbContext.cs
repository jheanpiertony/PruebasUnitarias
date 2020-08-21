using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Common.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-RFKKK95\SQLEXPRESS; Initial Catalog = PruebaUnitariasDB; Integrated Security = True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed
            builder.Entity<Actor>().HasData(
                new Actor()
                {
                    FechaNacimiento = new DateTime(1947, 7, 30),
                    Foto = String.Empty,
                    Id = 1,
                    Nombre = "Arnold Alois Schwarzenegger",
                },
                new Actor()
                {
                    FechaNacimiento = new DateTime(1946, 7, 6),
                    Foto = String.Empty,
                    Id = 2,
                    Nombre = "Michael Sylvester Gardenzio Stallone",
                },
                new Actor()
                {
                    FechaNacimiento = new DateTime(1974, 11, 11),
                    Foto = String.Empty,
                    Id = 3,
                    Nombre = "Leonardo Wilhelm DiCaprio​",
                });
        }
        public DbSet<Actor> Actores { get; set; }
    }
}

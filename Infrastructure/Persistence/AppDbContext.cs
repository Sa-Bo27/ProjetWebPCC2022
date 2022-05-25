using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; } // TODO A Verifier!
        public DbSet<MovieDom>? MoviesDm { get; set; } // TODO a verifier!


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //Todo add code for auditableEntity
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Define some FK for example
            builder.Entity<User>().HasMany(x => x.ListMovies);
            builder.Entity<MovieDom>().HasMany(x => x.Users);
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Bob",
                    Email = "Bob01@test.com",

                }
            );




            base.OnModelCreating(builder);
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data source=AppStore.db");

            return new AppDbContext(optionsBuilder.Options);
        }


    }
}
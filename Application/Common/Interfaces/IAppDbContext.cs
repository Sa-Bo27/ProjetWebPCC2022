using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
         public DbSet<User> Users {get; set;} // TODO A Verifier!
        public DbSet<MovieDom> MoviesDm {get; set;} // TODO a verifier!


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) ;
    }
}
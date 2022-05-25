using MediatR;
using Domain.Entites;
using Application.DTO;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetUserQuery : IRequest<User>
    {

        public string Email { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IAppDbContext _context;
        public GetUserQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {

            if (request == null) throw new NotFoundException(nameof(GetUserQuery), request);



            var userById = await _context.Users.Where(x => x.Email == request.Email).Include(x => x.ListMovies).FirstOrDefaultAsync();

            if (userById == null) throw new NotFoundException(nameof(User), request.Email);

            return userById;


        }
    }
}
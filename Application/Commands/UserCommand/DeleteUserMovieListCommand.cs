using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.UserCommand
{
    public class DeleteUserMovieListCommand : IRequest
    {
        public int MovieId { get; set; }
        public string EmailUser {get; set;}
    }

    public class DeleteUserMovieListCommandHandler : IRequestHandler<DeleteUserMovieListCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteUserMovieListCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<Unit> Handle(DeleteUserMovieListCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Email.Contains(request.EmailUser)).FirstOrDefaultAsync();
            var movie = await _context.MoviesDm.FindAsync(request.MovieId);
            var userMovie = user.ListMovies.Where(x => x.Id == request.MovieId).FirstOrDefault();


            if(user == null) throw new NotFoundException(nameof(User),request.EmailUser);
            if(movie == null) throw new NotFoundException(nameof(MovieDom),request.MovieId);

            if(userMovie is not null) user.ListMovies.Remove(movie);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
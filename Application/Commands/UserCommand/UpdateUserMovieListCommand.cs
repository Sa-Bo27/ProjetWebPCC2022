using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTO;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Commands.UserCommand
{
    public class UpdateUserMovieListCommand : IRequest
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public MovieDto MovieDto { get; set; }
    }

    public class UpdateUserMovieListCommandHandler : IRequestHandler<UpdateUserMovieListCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateUserMovieListCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<Unit> Handle(UpdateUserMovieListCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Email);
            // var movie = await _context.MoviesDm.FindAsync(request.MovieDto.Id);
            var movie = request.MovieDto;
            
            if (user == null) {
                var newUser = new User{ Email= request.Email};
                _context.Users.Add(newUser);
                if(movie!=null) newUser.ListMovies.Add(_mapper.Map<MovieDto,MovieDom>(request.MovieDto));
                await _context.SaveChangesAsync(cancellationToken);
            };

            if(movie == null) throw new NotFoundException(nameof(MovieDto), request.MovieDto);


            user.ListMovies.Add(_mapper.Map<MovieDto,MovieDom>(request.MovieDto));
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
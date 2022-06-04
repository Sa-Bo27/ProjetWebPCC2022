using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTO;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var user = await _context?.Users?.FirstOrDefaultAsync(x => x.Email == request.Email);
            // var movie = await _context.MoviesDm.FindAsync(request.MovieDto.Id);
            
            

            if(request.MovieDto == null) throw new NotFoundException(nameof(MovieDto), request.MovieDto);
            
            if (user == null) {
                var newUser = new User{ Email= request.Email};
                newUser.AddMovie(_mapper.Map<MovieDto,MovieDom>(request.MovieDto));
                _context.Users?.Add(newUser);
                await _context.SaveChangesAsync(cancellationToken);
             
            };

            

            // var newMovie = new MovieDom
            // {
            //     Adult= request.MovieDto.Adult, 
            //     Backdrop_path=request.MovieDto.Backdrop_path,
            //     Id = request.MovieDto.Id,
            //     Original_language = request.MovieDto.Original_language,
            //     Original_title = request.MovieDto.Original_title,
            //     Overview = request.MovieDto.Overview,
            //     Popularity = request.MovieDto.Popularity,
            //     Poster_path = request.MovieDto.Poster_path,
            //     Release_date = request.MovieDto.Release_date,
            //     Title = request.MovieDto.Title
            // };

            // _context.MoviesDm.Add(newMovie);
            // await _context.SaveChangesAsync(cancellationToken);

            user.AddMovie(_mapper.Map<MovieDto,MovieDom>(request.MovieDto));
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
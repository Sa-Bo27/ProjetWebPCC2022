using MediatR;
using Domain.Entites;
using Application.DTO;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Application.RequestHelpers;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace Application.Queries
{
    public class GetUserListQuery : IRequest<List<MovieDto>>
    {

        public string Email { get; set; }
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<MovieDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetUserListQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<MovieDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            List<MovieDto> listMovie = new();

            if (request == null) throw new NotFoundException(nameof(GetUserListQuery), request);

            var userByEmail = await _context.Users.Where(x => x.Email == request.Email).Include(x => x.ListMovies).FirstOrDefaultAsync();

            if (userByEmail == null) throw new NotFoundException(nameof(User), request.Email);

            if(userByEmail.ListMovies == null) throw new NotFoundException(nameof(User), userByEmail.ListMovies);

            foreach (var movie in userByEmail.ListMovies)
            {
                listMovie.Add(_mapper.Map<MovieDom, MovieDto>(movie));
            }
            return listMovie;


        }

        // Task<PagedList<User>> IRequestHandler<GetUserQuery, PagedList<User>>.Handle(GetUserQuery request, CancellationToken cancellationToken)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
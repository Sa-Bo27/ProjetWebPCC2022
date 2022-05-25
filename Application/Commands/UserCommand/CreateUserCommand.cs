using Application.Common.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Commands.UserCommand
{
    public class CreateUserCommand : IRequest<int>
    {
        //TODO a implementer
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IAppDbContext _context;
        public CreateUserCommandHandler(IAppDbContext context)
        {
            _context = context;

        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //TODO a implementer

            var user = new User{
                Email = request.Email,
                Name = request.Name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;

        }
    }
}
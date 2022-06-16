using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Application.Mail;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
			private readonly IMailSender _mailSender;

			public Handler(DataContext context, IUserAccessor userAccessor, IMailSender mailSender)
            {
                _context = context;
                _userAccessor = userAccessor;
				_mailSender = mailSender;
			}

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUserName());

                var allUsers = _context.Users
                    .Select(c => new { c.DisplayName, c.Email })
                    .AsEnumerable()
                    .Select(c => new Tuple<string, string>(c.DisplayName, c.Email))
                    .ToList();

                var attendee = new ActivityAttendee
                {
                    ActivityId = Guid.NewGuid(),
                    AppUser = user,
                    Activity = request.Activity,
                    IsHost = true
                };

                request.Activity.Attendees.Add(attendee);

                _context.Activities.Add(request.Activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create activity");

           
                _mailSender.SendEmailsForActivity(allUsers, request.Activity);
               

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
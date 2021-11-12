using Application.Interfaces;
using Domain;
using MediatR;
using Royalty.Insurance.Settings;
using System;
using System.Common.Authentication.Models;
using System.Common.EmailSender;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserMapperService _mapper;
        private readonly IEmailSender _emailSender;

        public CreateUserProfileCommandHandler(IEmailSender emailSender, IUserMapperService mapper, IApplicationDbContext context)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            User user = new User();
            _mapper.UpdateEntity(user, request);
            user.TemporaryPassword = true;
            user.ActivationExpiryDatetimeUtc = DateTime.UtcNow.AddHours(48);
            await _context.Users.AddAsync(user, cancellationToken);

            var saved = await _context.SaveChangesAsync(cancellationToken) == 1;
            await _emailSender.Send(new EmailMessage(user.Email, ResourceCommonMessage.EmailActivationSubject,
                string.Format(ResourceCommonMessage.EmailActivationBody, request.Password)));

            return saved;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using LinqKit;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUsersIndividualUnreadMessagesQueryHandler : IRequestHandler<GetUsersIndividualUnreadMessagesQuery, List<GroupUnreadMessageResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersIndividualUnreadMessagesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupUnreadMessageResponse>> Handle(GetUsersIndividualUnreadMessagesQuery request, CancellationToken cancellationToken)
        {
            if(request.UnreadMessageSetting == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var query = _context.UnreadMessages
                .AsNoTracking()
                .AsExpandable();
            var predicate = PredicateBuilder.New<UnreadMessage>();

            var utcNow = DateTime.UtcNow;
            //Loop through the keywords
            predicate = request.UnreadMessageSetting.Aggregate(predicate, (current, item) =>
                current.Or(p => p.SendUserId.Equals(item.UserId)
                                &&
                                EF.Functions.DateDiffMinute(p.ReadDatetimeUtc, utcNow) >= item.UnReadPreferenceInMinutes));
            query = query.Where(predicate);
            var result = await query.Join(_context.Groups, unreadMessage => unreadMessage.GroupId,
                    group => group.Id,
                    (unreadMessage, group) =>
                        new
                        {
                            group.GroupTypeId,
                            GroupId = group.Id,
                            unreadMessage.SendUserId,
                            unreadMessage.MessageId
                        })
                    .GroupBy(item => new { item.GroupTypeId, item.GroupId, item.SendUserId })
                    .Where(item => item.Key.GroupTypeId == (int)GroupTypeCode.Individual)
                    .Select(item => new
                    {
                        item.Key.GroupId,
                        item.Key.GroupTypeId,
                        item.Key.SendUserId,
                        UnReadMessageCount = item.Count(),
                        MessageId = item.Max(element => element.MessageId)
                    })
                    .Join(_context.Messages, unreadMessage => unreadMessage.MessageId,
                        message => message.Id,
                        (unreadMessage, message) =>
                            new GroupUnreadMessageResponse
                            {
                                GroupTypeId = unreadMessage.GroupTypeId,
                                GroupId = unreadMessage.GroupId,
                                SendUserId = unreadMessage.SendUserId,
                                UnreadMessageCount = unreadMessage.UnReadMessageCount,
                                LastMessage = message.Body,
                                LastMessageDate = message.CreateDatetimeUtc,
                                MessageId = message.Id
                            })
                    .ToListAsync(cancellationToken);


            return result;
        }
    }
}

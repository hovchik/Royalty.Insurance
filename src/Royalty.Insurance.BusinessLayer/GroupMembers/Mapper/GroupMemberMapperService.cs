using System;
using System.Common.Authentication.Models;
using System.Linq;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class GroupMemberMapperService : IGroupMemberMapperService
    {
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly AppSetting _appSetting;

        public GroupMemberMapperService(IExpiryQueryParameterCreator expiryQueryParameterCreator, IOptions<AppSetting> options)
        {
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _appSetting = options.Value;
        }

        public void UpdateEntity(GroupMember entity, int memberId)
        {
            entity.MemberId = memberId;
        }

        public Expression<Func<Group, GroupMemberResponse>> MapResponse
        {
            get
            {
                return (entity) => new GroupMemberResponse
                {
                    GroupId = entity.Id,
                    GroupCreatedById = entity.CreatedBy,
                    GroupName = entity.Name,
                    GroupTypeId = entity.GroupTypeId,
                    Members = entity.GroupMembers.Select(item =>
                    new MemberResponse(_expiryQueryParameterCreator, _appSetting)
                    {
                        Status = item.Member.UsersProfile == null ? (int)UserStatusCode.Offline : item.Member.UsersProfile.UserStatusId,
                        PersonalAvatar = item.Member.PersonalAvatar,
                        UnreadMessageCount = item.Group.UnreadMessages.GroupBy(message => new { message.MessageId, message.SendUserId }).Count(user => user.Key.SendUserId.Equals(item.MemberId)),
                        UserId = item.MemberId,
                        LastMessage = item.Group.Messages.OrderBy(message => message.CreateDatetimeUtc).LastOrDefault() == null ? string.Empty : item.Group.Messages.OrderBy(message => message.CreateDatetimeUtc).Last().Body,
                        LastMessageDate = item.Group.Messages.OrderBy(message => message.CreateDatetimeUtc).LastOrDefault() == null ? (DateTime?)null : item.Group.Messages.OrderBy(message => message.CreateDatetimeUtc).Last().CreateDatetimeUtc,
                        Muted = item.Muted,
                        MemberFullName = $"{item.Member.FirstName} {item.Member.LastName}"
                    }).ToList()
                };
            }
        }
    }
}

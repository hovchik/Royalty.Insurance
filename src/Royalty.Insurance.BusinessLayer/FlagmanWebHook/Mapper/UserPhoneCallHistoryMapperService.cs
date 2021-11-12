using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public class UserPhoneCallHistoryMapperService : IUserPhoneCallHistoryMapperService
    {
        public void ModifyEntity(UserPhoneCallHistory entity, CreateCallRecordCommand request)
        {
            switch (request.CallType)
            {
                case CallTypeCode.Missed:
                    entity.InitialCallTypeId = (byte)CallTypeCode.Missed;
                    break;
                case CallTypeCode.Established:
                    entity.CreateDatetimeUtc = DateTime.UtcNow;
                    entity.CurrentCallTypeId = (byte)CallTypeCode.Established;
                    break;
                case CallTypeCode.Terminated:
                    entity.EndDatetimeUtc = DateTime.UtcNow;
                    break;
            }

        }

        public void UpdateEntity(UserPhoneCallHistory entity, CreateCallRecordCommand request)
        {
            entity.UserPhoneId = request.UserPhoneId;
            entity.InitialCallTypeId = (byte)request.CallType;
            entity.CallerNumber = request.CallNumber;
            entity.Extension = request.Extension;
            entity.CallId = request.CallId.Trim();
            entity.CallerName = request.CallerName;
            entity.CurrentCallTypeId = (byte)request.CallType;
        }

        public Expression<Func<UserPhoneCallHistory, UserPhoneCallHistoryResponse>> MapResponse
        {
            get
            {
                return entity => new UserPhoneCallHistoryResponse
                {
                    CallType = (CallTypeCode)entity.InitialCallTypeId,
                    CallNumber = entity.CallerNumber,
                    UserId = entity.UserPhoneId,
                    Extension = entity.Extension,
                    CreationTime = entity.CreateDatetimeUtc,
                    CallerName = entity.CallerName,
                    CallId = entity.CallId,
                    Duration = entity.CurrentCallTypeId == (byte)CallTypeCode.Established ? EF.Functions.DateDiffSecond(entity.CreateDatetimeUtc, entity.EndDatetimeUtc) : 0
                };
            }
        }
    }
}

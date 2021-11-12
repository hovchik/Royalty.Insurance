using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public interface IUserPhoneCallHistoryMapperService
    {
        void UpdateEntity(UserPhoneCallHistory entity, CreateCallRecordCommand request);
        void ModifyEntity(UserPhoneCallHistory entity, CreateCallRecordCommand request);
        Expression<Func<UserPhoneCallHistory, UserPhoneCallHistoryResponse>> MapResponse { get; }
    }
}

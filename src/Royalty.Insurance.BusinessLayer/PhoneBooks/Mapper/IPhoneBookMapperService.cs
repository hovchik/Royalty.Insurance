using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public interface IPhoneBookMapperService
    {
        void UpdateEntity(PhoneBook entity, CreatePhoneCommand request);
        Expression<Func<PhoneBook, PhoneBookResponse>> MapResponse { get; }

        void UpdateEntity(PhoneBook entity, UpdatePhoneCommand request);
    }
}
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class PhoneBookMapperService : IPhoneBookMapperService
    {
        public void UpdateEntity(PhoneBook entity, CreatePhoneCommand request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.PhoneNumber = request.Number;
        }

        public void UpdateEntity(PhoneBook entity, UpdatePhoneCommand request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.PhoneNumber = request.Number;
        }

        public Expression<Func<PhoneBook, PhoneBookResponse>> MapResponse => entity => new PhoneBookResponse
        {
            LastName = entity.LastName,
            FirstName = entity.FirstName,
            Number = entity.PhoneNumber,
            Id = entity.Id
        };
    }
}
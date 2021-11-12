using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.MapperService
{
    public class ZipMapperService: IZipMapperService
    {
        public void UpdateEntity(ZipCode entity, ZipcodeRequest request)
        {
            entity.Code = request.Code;
        }

        public Expression<Func<ZipCode, ZipcodeResponse>> MapResponse
        {
            get
            {
                return entity => new ZipcodeResponse
                {
                    City = entity.City.Name,
                    Code = entity.Code,
                    Id = entity.Id
                };
            }
        }
    }
}
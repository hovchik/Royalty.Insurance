using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.MapperService
{
    public interface IZipMapperService
    {
        void UpdateEntity(ZipCode entity, ZipcodeRequest request);
        Expression<Func<ZipCode, ZipcodeResponse>> MapResponse { get; }
    }
}
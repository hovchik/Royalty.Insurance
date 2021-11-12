using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public interface INoteMapperService
    {
        void UpdateEntity(Note entity, NoteRequest request);
        Expression<Func<Note, NoteResponse>> MapResponse { get; }
    }
}

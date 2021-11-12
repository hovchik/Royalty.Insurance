using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class NoteMapperService : INoteMapperService
    {
        public Expression<Func<Note, NoteResponse>> MapResponse
        {
            get
            {
                return entity => new NoteResponse
                {
                    UserId = entity.UserId,
                    Note = entity.Description,
                    InsuredId = entity.InsuredId,
                    CreatedDateTime = entity.CreateDateTime,
                    Id = entity.Id
                };
            }

        }

        public void UpdateEntity(Note entity, NoteRequest request)
        {
            entity.InsuredId = request.InsuredId;
            entity.Description = request.Note;
        }
    }
}

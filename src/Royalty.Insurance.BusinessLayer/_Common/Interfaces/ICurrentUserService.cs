using System;

namespace Royalty.Insurance.BusinessLayer.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }

        string UserFullName { get; }
        Guid SessionId { get; }

        bool IsSupperAdmin { get; }

        string UserEmail { get; }
    }
}

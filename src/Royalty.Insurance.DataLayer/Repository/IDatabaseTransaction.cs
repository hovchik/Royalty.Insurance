using System;
using System.Threading.Tasks;

namespace Royalty.Insurance.DataLayer.Repository
{
    public interface IDatabaseTransaction: IDisposable
    {
        Task Commit();
        Task Rollback();
    }
}
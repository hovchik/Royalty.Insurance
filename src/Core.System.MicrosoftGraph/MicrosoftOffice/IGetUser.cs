using System.Threading;
using System.Threading.Tasks;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public interface IGetUser
    {
        Task<MicrosoftOfficeUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken);
    }
}

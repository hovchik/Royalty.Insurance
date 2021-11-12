using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice
{
    public class GetUserQuery : GetUserRequest, IRequest<MicrosoftOfficeUserResponse>
    {
    }
}

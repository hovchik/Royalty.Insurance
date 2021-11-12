
using System.Net.Http;

namespace System.Common.Network
{
     public interface IErrorHandling
     {
         void Handle(HttpResponseMessage responseMessage);
     }
}

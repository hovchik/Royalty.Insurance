using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Royalty.Insurance.Api.Controllers
{

    public abstract class BaseController<T> : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger<T> Logger;


        /// <summary>
        /// Constrictor
        /// </summary>
        /// <param name="logger">logger</param>
        protected BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}

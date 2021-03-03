using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PictureStore.Web.Api.Models;

namespace PictureStore.Web.Api.Controllers
{
    [Route("error")]
    public class ErrorController : BaseController
    {
        private readonly IWebHostEnvironment env;

        public ErrorController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [NonAction]
        [Route("")]
        public ErrorResponseModel Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return new ErrorResponseModel(exception, env.IsDevelopment()); // Your error model
        }
    }
}

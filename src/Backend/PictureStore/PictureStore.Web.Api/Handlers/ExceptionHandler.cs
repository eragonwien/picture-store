using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using PictureStore.Core.Exceptions;
using PictureStore.Web.Api.Models;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PictureStore.Web.Api.Handlers
{
   public class ExceptionHandler
   {
      public async Task HandleExceptionAsync(HttpContext context, IHostEnvironment env)
      {
         var errorContext = context.Features.Get<IExceptionHandlerPathFeature>();

         if (errorContext is null) return;

         var statusCode = HttpStatusCode.InternalServerError;
         string message = null; ;
         string stackTrace = null;

         switch (errorContext.Error)
         {
            case UploadFileEmptyException uploadFileEmptyException:
               statusCode = HttpStatusCode.BadRequest;
               message = "Input file is required";
               break;
            case UploadStorageNotFoundException uploadStorageUnreachableException:
               statusCode = HttpStatusCode.BadRequest;
               message = "Storage cannot be reached. Please make sure that the hard drive is plugged in";
               break;
            case FileNotFoundException fileNotFoundException:
               statusCode = HttpStatusCode.NotFound;
               message = "File not found";
               break;
            case MoveToDownloadFolderException moveToDownloadFolderException:
               statusCode = HttpStatusCode.InternalServerError;
               message = $"Error transfering file to download folder: {moveToDownloadFolderException.Message}";
               break;
            case TaskCanceledException taskCanceledException:
               statusCode = HttpStatusCode.InternalServerError;
               message = "Request was cancelled by the server. Please try again later";
               break;
            default:
               statusCode = HttpStatusCode.InternalServerError;
               message = $"Please contact admin";
               break;
         }

         context.Response.StatusCode = (int)statusCode;
         context.Response.ContentType = "application/json";

         if (!env.IsDevelopment())
            stackTrace = null;

         var result = new ErrorResponseModel(message, stackTrace);
         await context.Response.WriteAsJsonAsync(result);
      }
   }
}

using PictureStore.Core.Exceptions;
using System;
using System.IO;
using System.Linq;

namespace PictureStore.Web.Api.Models
{
   public class ErrorResponseModel
   {
      public string Type { get; set; }
      public string Message { get; set; }
      public string StackTrace { get; set; }

      public ErrorResponseModel(Exception exception, bool isDevelopment)
      {
         Type = exception.GetType().Name;
         Message = exception.Message;
         StackTrace = isDevelopment ? exception.StackTrace : null;

         HandleException(exception);
      }

      private void HandleException(Exception exception)
      {
         switch (exception)
         {
            case FileNotFoundException fileNotFoundException:
               Message = "File not found";
               break;
            case UploadFileEmptyException uploadFileEmptyException:
               Message = "Input file is required";
               break;
            case UploadStorageNotFoundException uploadStorageUnreachableException:
               Message = "Storage cannot be reached. Please make sure that the hard drive is plugged in";
               break;
            case MoveToDownloadFolderException moveToDownloadFolderException:
               Message = $"Error transfering file to download folder: {moveToDownloadFolderException.Message}";
               break;
            default:
               break;
         }
      }
   }
}

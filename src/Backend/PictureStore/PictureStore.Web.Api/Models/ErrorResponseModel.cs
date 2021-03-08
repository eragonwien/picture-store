using PictureStore.Core;
using System;
using System.Linq;
using System.Runtime.ExceptionServices;

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
                case MoveToDownloadFolderException e1:
                    Message = $"Unable to move following files: {string.Join(", ", e1.Errors.Select(e => e.SourcePath))}. Reason: {string.Join(", ", e1.Errors.Select(e => e.ErrorMessage).Distinct())}";
                    break;
                default:
                    break;
            }
        }
    }
}

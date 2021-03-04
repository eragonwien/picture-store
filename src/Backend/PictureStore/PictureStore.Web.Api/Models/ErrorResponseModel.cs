using System;

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
            StackTrace = !isDevelopment ? exception.StackTrace : null;
        }
    }
}

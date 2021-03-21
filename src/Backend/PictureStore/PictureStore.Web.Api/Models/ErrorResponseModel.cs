using Newtonsoft.Json;
using PictureStore.Core.Exceptions;
using System;
using System.IO;

namespace PictureStore.Web.Api.Models
{
   public class ErrorResponseModel
   {
      public string Message { get; set; }

      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      public string StackTrace { get; set; }

      public ErrorResponseModel(string message, string stackTrace)
      {
         Message = message;
         StackTrace = stackTrace;
      }
   }
}

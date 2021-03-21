using System;

namespace PictureStore.Core.Exceptions
{
   public class MoveToDownloadFolderException : Exception
   {
      public MoveToDownloadFolderException(Exception ex)
         : base("Error on transfering file(s)", ex)
      {

      }
   }
}

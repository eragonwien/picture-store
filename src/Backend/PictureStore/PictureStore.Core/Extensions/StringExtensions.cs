﻿using System.IO;

namespace PictureStore.Core.Extensions
{
   public static class StringExtensions
   {
      #region File

      public static string GetParentDirectory(this string path)
      {
         if (string.IsNullOrWhiteSpace(path))
            return string.Empty;

         return Path.GetFileName(Path.GetDirectoryName(path));
      }

      #endregion
   }
}
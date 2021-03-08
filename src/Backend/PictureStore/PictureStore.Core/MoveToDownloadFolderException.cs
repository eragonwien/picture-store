using PictureStore.Core.Models;
using System;
using System.Collections.Generic;

namespace PictureStore.Core
{
    public class MoveToDownloadFolderException : Exception
    {
        public MoveToDownloadFolderException()
        {
        }

        public MoveToDownloadFolderException(string message)
            : base(message)
        {
        }

        public MoveToDownloadFolderException(List<MovingFileError> errors)
        {
            Errors = errors;
        }

        public MoveToDownloadFolderException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public List<MovingFileError> Errors { get; }
    }
}

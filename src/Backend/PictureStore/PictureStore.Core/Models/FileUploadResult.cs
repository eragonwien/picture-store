using System;
using System.Collections.Generic;

namespace PictureStore.Core.Models
{
    public class FileUploadResult
    {
        public int TotalCount { get; set; }

        public int SuccessCount { get; set; }

        public List<FileUploadError> Errors { get; set; } = new();

        public FileUploadResult Add(FileUploadPartialResult partialResult)
        {
            if (partialResult is null) return this;

            if (partialResult.Succeed)
                AddSuccessResult(partialResult);
            else
                AddErrors(partialResult);

            return this;
        }

        private void AddSuccessResult(FileUploadPartialResult partialResult)
        {
            if (partialResult is null) return;

            TotalCount++;
            SuccessCount++;
        }

        private void AddErrors(FileUploadPartialResult partialResult)
        {
            if (partialResult is null) return;

            TotalCount++;
            Errors.Add(new FileUploadError(partialResult.FileName, partialResult.Message));
        }
    }
}

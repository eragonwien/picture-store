using System;
using System.Collections.Generic;

namespace PictureStore.Core.Models
{
    public class FileUploadResult
    {
        public int TotalCount { get; set; }

        public int SuccessCount { get; set; }

        public int FailCount { get; set; }

        public List<string> FailedFiles { get; set; } = new();

        public FileUploadResult Add(FileUploadPartialResult partialResult)
        {
            if (partialResult is null) return this;

            if (partialResult.Succeed)
                AddSuccessResult(partialResult);
            else
                AddFailedResult(partialResult);

            return this;
        }

        private void AddSuccessResult(FileUploadPartialResult partialResult)
        {
            if (partialResult is null) return;

            TotalCount++;
            SuccessCount++;
        }

        private void AddFailedResult(FileUploadPartialResult partialResult)
        {
            if (partialResult is null) return;

            TotalCount++;
            FailCount++;
            FailedFiles.Add(partialResult.FileName);
        }
    }
}

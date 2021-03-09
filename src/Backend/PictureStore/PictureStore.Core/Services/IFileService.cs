using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PictureStore.Core.Models;

namespace PictureStore.Core.Services
{
    public interface IFileService
    {
        Task<FileUploadPartialResult> UploadAsync(
            string inputFileName,
            Stream stream,
            CancellationToken cancellationToken);

        FileListingModel ListFiles(string folder, CancellationToken cancellationToken);

        Task DownloadAsync(string id);

        Task MoveToDownloadFolderAsync(CancellationToken cancellationToken);
        Task<IEnumerable<DuplicateFileModel>> ListDuplicatesAsync(CancellationToken cancellationToken);
    }
}

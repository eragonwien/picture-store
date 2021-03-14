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

        Task<DownloadFileModel> DownloadAsync(string folder, string filename, CancellationToken cancellationToken);

        Task TransferFileToDownloadFolderAsync(CancellationToken cancellationToken);

        Task<IEnumerable<DuplicateFileModel>> ListDuplicatesAsync(CancellationToken cancellationToken);

        IEnumerable<string> ListFolders(string folder);
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PictureStore.Core.Models
{
    public class DuplicateFileModel
    {
        public DuplicateFileModel(string fileHash, IEnumerable<string> files)
        {
            FileHash = fileHash;
            Files.AddRange(files);
        }

        [JsonIgnore]
        public string FileHash { get; set; }

        public List<string> Files { get; set; } = new();

        [JsonIgnore]
        public bool HasDuplicate => Files.Count > 1;
    }
}
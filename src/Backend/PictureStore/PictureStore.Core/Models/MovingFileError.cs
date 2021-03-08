namespace PictureStore.Core.Models
{
    public class MovingFileError
    {
        public string SourcePath { get; set; }

        public string DestinationPath { get; set; }

        public string ErrorMessage { get; set; }

        public MovingFileError(string sourcePath, string destinationPath, string errorMessage)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            ErrorMessage = errorMessage;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VHS.System.FilesystemLayer
{
    public class FilesystemLayer : IFilesystemLayer
    {
        private readonly FilesystemDirectoryExceptionHandlerWrapper _directoryExceptionHandlerWrapper =
            new FilesystemDirectoryExceptionHandlerWrapper();


        public List<string> GetAllFilesInDirectory(string directoryName)
        {
            CheckIfFilenameIsNullOrEmpty(directoryName);
            var files = _directoryExceptionHandlerWrapper.HandleExceptions(() => Directory.GetFiles(directoryName));
            return files?.ToList() ?? new List<string>();
        }

        public List<string> GetAllSubdirectoriesInDirectory(string directoryName)
        {
            CheckIfFilenameIsNullOrEmpty(directoryName);
            var subdirectories =  _directoryExceptionHandlerWrapper.HandleExceptions(() => Directory.GetDirectories(directoryName));
            return subdirectories?.ToList() ?? new List<string>();
        }

        private void CheckIfFilenameIsNullOrEmpty(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.IncorrectName);
            }
        }
    }
}
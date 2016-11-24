using System.Collections.Generic;
using System.IO;

namespace VSH.System.Filesystem
{
    public static class FilesystemLayer
    {
        private static readonly FilesystemDirectoryExceptionHandlerWrapper DirectoryExceptionHandlerWrapper =
            new FilesystemDirectoryExceptionHandlerWrapper();


        public static IEnumerable<string> GetAllFilesInDirectory(string directoryName)
        {
            CheckIfFilenameIsNullOrEmpty(directoryName);
            return DirectoryExceptionHandlerWrapper.HandleExceptions(() => Directory.GetFiles(directoryName));
        }

        public static IEnumerable<string> GetAllSubdirectoriesInDirectory(string directoryName)
        {
            CheckIfFilenameIsNullOrEmpty(directoryName);
            return DirectoryExceptionHandlerWrapper.HandleExceptions(() => Directory.GetDirectories(directoryName));
        }

        private static void CheckIfFilenameIsNullOrEmpty(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.IncorrectName);
            }
        }
    }
}
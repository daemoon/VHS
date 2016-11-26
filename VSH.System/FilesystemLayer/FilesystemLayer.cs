using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VHS.System.FilesystemLayer
{
    public class FilesystemLayer : IFilesystemLayer
    {
        private readonly FilesystemExceptionHandlerWrapper _fslExceptionHandlerWrapper =
            new FilesystemExceptionHandlerWrapper();


        public List<string> GetAllFilesInDirectory(string directoryName)
        {
            CheckIfFilenameIsNullOrEmpty(directoryName);
            var files = _fslExceptionHandlerWrapper.HandleExceptions(() => Directory.GetFiles(directoryName));
            return files?.ToList() ?? new List<string>();
        }

        public List<string> GetAllSubdirectoriesInDirectory(string directoryName)
        {
            CheckIfFilenameIsNullOrEmpty(directoryName);
            var subdirectories =
                _fslExceptionHandlerWrapper.HandleExceptions(() => Directory.GetDirectories(directoryName));
            return subdirectories?.ToList() ?? new List<string>();
        }

        public byte[] GetContentsOfFile(string fileName)
        {
            CheckIfFilenameIsNullOrEmpty(fileName);
            return _fslExceptionHandlerWrapper.HandleExceptions(() => File.ReadAllBytes(fileName));
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
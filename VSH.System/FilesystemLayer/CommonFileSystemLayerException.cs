using System;
using System.Collections.Generic;

namespace VHS.System.FilesystemLayer
{
    public abstract class FilesystemLayerException : Exception
    {
        protected FilesystemLayerException(string message) : base(message)
        {

        }
    }

    public class CommonFileSystemLayerException : FilesystemLayerException
    {
        public enum FSLExceptionType
        {
            UnableToAccess,
            IncorrectName,
            PathIsFileOrIOError,
            DirectoryNotFound
        }

        private static readonly Dictionary<FSLExceptionType, string> TypeToMessageMapping = new Dictionary
            <FSLExceptionType, string>()
            {
                {FSLExceptionType.IncorrectName, "Given path seems to be incorrect."},
                {FSLExceptionType.UnableToAccess, "You are unable to access this directory."},
                {FSLExceptionType.PathIsFileOrIOError, "Seems that path is pointing to file or that you've encountered I/O error."},
                {FSLExceptionType.DirectoryNotFound, "Directory not found" }

            };

        public CommonFileSystemLayerException(FSLExceptionType type) : base(TypeToMessageMapping[type])
        {
        }
    }

    public class FileSystemLayerFileNotFoundException : FilesystemLayerException
    {

        public FileSystemLayerFileNotFoundException() : base("File with given path was not found.")
        {
        }
    }
}
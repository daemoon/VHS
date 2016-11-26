using System;
using System.Collections.Generic;

namespace VHS.System.FilesystemLayer
{
    public class FilesystemLayerException : Exception
    {
        public enum FSLExceptionType
        {
            UnableToAccess,
            IncorrectName,
            PathIsFileOrIOError,
            FileOrDirectoryNotFound
        }

        private static readonly Dictionary<FSLExceptionType, string> TypeToMessageMapping = new Dictionary
            <FSLExceptionType, string>()
            {
                {FSLExceptionType.IncorrectName, "Given path seems to be incorrect."},
                {FSLExceptionType.UnableToAccess, "You are unable to access this directory."},
                {FSLExceptionType.PathIsFileOrIOError, "Seems that path is pointing to file or that you've encountered I/O error."},
                {FSLExceptionType.FileOrDirectoryNotFound, "File with given path was not found."}
            };

        public FilesystemLayerException(FSLExceptionType type) : base(TypeToMessageMapping[type])
        {
        }
    }
}
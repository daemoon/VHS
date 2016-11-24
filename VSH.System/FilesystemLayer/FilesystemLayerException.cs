using System;
using System.Collections.Generic;

namespace VHS.System.FilesystemLayer
{
    public class FilesystemLayerException : Exception
    {
        public enum FSLExceptionType
        {
            UnableToAccessDirectory,
            IncorrectName,
            PathIsFile
        }

        private static readonly Dictionary<FSLExceptionType, string> TypeToMessageMapping = new Dictionary
            <FSLExceptionType, string>()
            {
                {FSLExceptionType.IncorrectName, "Given path seems to be incorrect."},
                {FSLExceptionType.UnableToAccessDirectory, "You are unable to access this directory."},
                {FSLExceptionType.PathIsFile, "Seems that path is pointing to file while path must be directory."}
            };

        public FilesystemLayerException(FSLExceptionType type) : base(TypeToMessageMapping[type])
        {
        }
    }
}
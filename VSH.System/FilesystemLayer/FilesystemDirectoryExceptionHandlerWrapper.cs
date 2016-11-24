using System;
using System.IO;

namespace VHS.System.FilesystemLayer
{
    public class FilesystemDirectoryExceptionHandlerWrapper
    {
        public T HandleExceptions<T>(Func<T> method)
        {
            try
            {
                return method();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is PathTooLongException)
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.IncorrectName);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is DirectoryNotFoundException)
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.UnableToAccessDirectory);
            }
        }
    }
}
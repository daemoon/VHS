using System;
using System.IO;
using System.Security;

namespace VHS.System.FilesystemLayer
{
    public class FilesystemExceptionHandlerWrapper
    {
        public T HandleExceptions<T>(Func<T> method)
        {
            try
            {
                return method();
            }
            catch (Exception ex)
                when (ex is ArgumentException || ex is PathTooLongException || ex is NotSupportedException)
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.IncorrectName);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.UnableToAccess);
            }
            catch (Exception ex) when (ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.FileOrDirectoryNotFound);
            }
            catch (IOException)
            {
                throw new FilesystemLayerException(FilesystemLayerException.FSLExceptionType.PathIsFileOrIOError);
            }
        }
    }
}
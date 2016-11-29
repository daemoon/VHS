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
                throw new CommonFileSystemLayerException(CommonFileSystemLayerException.FSLExceptionType.IncorrectName);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
            {
                throw new CommonFileSystemLayerException(CommonFileSystemLayerException.FSLExceptionType.UnableToAccess);
            }
            catch (Exception ex) when (ex is DirectoryNotFoundException)
            {
                throw new CommonFileSystemLayerException(
                    CommonFileSystemLayerException.FSLExceptionType.DirectoryNotFound);
            }
            catch (FileNotFoundException)
            {
                throw new FileSystemLayerFileNotFoundException();
            }
            catch (IOException)
            {
                throw new CommonFileSystemLayerException(CommonFileSystemLayerException.FSLExceptionType.PathIsFileOrIOError);
            }
        }
    }
}
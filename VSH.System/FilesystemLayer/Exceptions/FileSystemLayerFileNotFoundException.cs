namespace VHS.System.FilesystemLayer.Exceptions
{
    public class FileSystemLayerFileNotFoundException : FilesystemLayerException
    {

        public FileSystemLayerFileNotFoundException() : base("File with given path was not found.")
        {
        }
    }
}
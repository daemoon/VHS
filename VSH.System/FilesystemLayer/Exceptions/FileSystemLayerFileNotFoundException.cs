namespace VHS.System.FilesystemLayer
{
    public class FileSystemLayerFileNotFoundException : FilesystemLayerException
    {

        public FileSystemLayerFileNotFoundException() : base("File with given path was not found.")
        {
        }
    }
}
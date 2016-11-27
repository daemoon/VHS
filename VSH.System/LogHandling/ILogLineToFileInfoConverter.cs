namespace VHS.System
{
    public interface ILogLineToFileInfoConverter
    {
        FileInfoCollector.FileInfo Convert(string logLine);
    }
}
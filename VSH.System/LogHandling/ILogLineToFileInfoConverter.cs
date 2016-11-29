namespace VHS.System
{
    public interface ILogLineToFileInfoConverter
    {
        FileInfoCollector.FileInformations Convert(string logLine);
        string Revert(FileInfoCollector.FileInformations fileInformations);
    }
}
using VHS.System.FileInfoCollectors;

namespace VHS.System.LogHandling.Converters.Single
{
    public interface ILogLineToFileInfoConverter
    {
        FileInfoCollector.FileInformations Convert(string logLine);
        string Revert(FileInfoCollector.FileInformations fileInformations);
    }
}
namespace VHS.System
{
    public class LogLineToFileInfoConverter : ILogLineToFileInfoConverter
    {
        public FileInfoCollector.FileInfo Convert(string logLine)
        {
            var splittedLine = logLine.Split('|');

            return new FileInfoCollector.FileInfo() {FilePath = splittedLine[0], Hash = splittedLine[1]};
        }
    }
}
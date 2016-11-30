namespace VHS.System
{
    public class LogLinesToFileInfoConverter : ILogLineToFileInfoConverter
    {
        public char Delimeter = '|';

        public FileInfoCollector.FileInformations Convert(string logLine)
        {
            var splittedLine = logLine.Split(Delimeter);

            return new FileInfoCollector.FileInformations() {FilePath = splittedLine[0], Hash = splittedLine[1]};
        }

        public string Revert(FileInfoCollector.FileInformations fileInformations)
        {
            return fileInformations.FilePath + Delimeter + fileInformations.Hash;
        }

        
    }
}
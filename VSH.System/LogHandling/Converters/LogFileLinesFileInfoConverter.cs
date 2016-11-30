using System.Collections.Generic;
using System.Linq;
using VHS.System.FileInfoCollectors;
using VHS.System.LogHandling.Converters.Single;

namespace VHS.System.LogHandling.Converters
{
    public class LogFileLinesFileInfoConverter
    {
        private ILogLineToFileInfoConverter _converter;

        public LogFileLinesFileInfoConverter(ILogLineToFileInfoConverter converter)
        {
            _converter = converter;
        }

        public List<FileInfoCollector.FileInformations> ConvertAll(List<string> logLines )
        {
            return logLines.Select(x => _converter.Convert(x)).ToList();
        }

        public List<string> RevertAll(ICollection<FileInfoCollector.FileInformations> fileInfos)
        {
            return fileInfos.Select(x => _converter.Revert(x)).ToList();
        }
    }
}
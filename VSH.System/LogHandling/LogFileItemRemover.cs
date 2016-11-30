using System.Collections.Generic;
using System.Linq;
using VHS.System.FileInfoCollectors;
using VHS.System.LogHandling.LogFileNameProvider;

namespace VHS.System.LogHandling
{
    public class LogFileItemRemover
    {
        private readonly ILogFileNameProvider _lfnp;

        public LogFileItemRemover(ILogFileNameProvider lfnp)
        {
            _lfnp = lfnp;
        }

        public List<FileInfoCollector.FileInformations> Remove(List<FileInfoCollector.FileInformations> informations)
        {
            var logFileName = _lfnp.GetLogFileName();
            return informations.Where(x => x.FilePath.Length < logFileName.Length || x.FilePath.Substring(x.FilePath.Length - logFileName.Length) != logFileName).ToList();

        }
    }
}
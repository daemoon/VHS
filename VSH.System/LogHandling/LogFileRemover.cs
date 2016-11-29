using System;
using System.Collections.Generic;
using System.Linq;

namespace VHS.System
{
    public class LogFileRemover
    {
        private readonly ILogFileNameProvider _lfnp;

        public LogFileRemover(ILogFileNameProvider lfnp)
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
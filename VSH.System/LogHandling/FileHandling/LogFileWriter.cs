using System.Collections.Generic;
using VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class LogFileWriter
    {
        private readonly IFilesystemLayer _fsl;
        private readonly LogFileLinesFileInfoConverter _fi2ll;
        private readonly ILogFileNameProvider _lfnp;

        public LogFileWriter(IFilesystemLayer fsl, LogFileLinesFileInfoConverter fi2Ll, ILogFileNameProvider lfnp)
        {
            _fsl = fsl;
            _fi2ll = fi2Ll;
            _lfnp = lfnp;
        }

        public void WriteLog(ICollection<FileInfoCollector.FileInformations> fileInfo, string path)
        {
            _fsl.WriteFile(_fi2ll.RevertAll(fileInfo), path + @"/" + _lfnp.GetLogFileName());
        }
    }
}
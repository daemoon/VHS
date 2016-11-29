using System.Collections.Generic;
using System.Linq;
using VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class LogFileGatherer 
    {
        private IFilesystemLayer _fsl;
        private ILogFileNameProvider _lfnp;

        public LogFileGatherer(IFilesystemLayer fsl, ILogFileNameProvider lfnp)
        {
            _fsl = fsl;
            _lfnp = lfnp;
        }

        public List<string> GetInfoLinesFromLog(string path)
        {
            List<string> infoLines;
            var logFileFullPath = path + @"\" + _lfnp.GetLogFileName();
            string[] lines;
            try
            {
                lines = _fsl.GetStringLinesOfFile(logFileFullPath);
            }
            catch (FileSystemLayerFileNotFoundException)
            {
                throw new LfgLogFileDoesntExistException();
            }
            if (lines == null || lines.Length == 0)
            {
                infoLines = new List<string>();
            }
            else
            {
                infoLines = lines.ToList();
            }
            return infoLines;
        }
    }
}
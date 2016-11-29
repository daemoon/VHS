using System.Collections.Generic;
using System.Linq;
using VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class LogFileGatherer
    {
        private IFilesystemLayer _fsl;
        private const string LogFileName = ".vhs";

        public LogFileGatherer(IFilesystemLayer fsl)
        {
            _fsl = fsl;
        }

        public List<string> GetInfoLinesFromLog(string path)
        {
            List<string> infoLines;
            var logFileFullPath = path + LogFileName;
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
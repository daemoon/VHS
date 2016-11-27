using System.Collections.Generic;
using VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class LogInfoGatherer
    {
        private IFilesystemLayer _fsl;
        private LogFileGatherer _lfg;
        private LogFileLinesFileInfoConverter _il2fi;

        public LogInfoGatherer(FilesystemLayer.IFilesystemLayer fsl)
        {
            _fsl = fsl;
            _lfg = new LogFileGatherer(fsl);
            _il2fi = new LogFileLinesFileInfoConverter(new LogLineToFileInfoConverter());
        }

        public List<FileInfoCollector.FileInfo> GetFileInfoLogFromPath(string path)
        {

            //TODO: unit test this
            var infoLines = _lfg.GetInfoLinesFromLog(path);
            return _il2fi.ConvertAll(infoLines);
        }
    }
}
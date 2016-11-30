using System.Collections.Generic;
using VHS.System.FileInfoCollectors;
using VHS.System.FilesystemLayer;
using VHS.System.LogHandling.Converters;
using VHS.System.LogHandling.Converters.Single;
using VHS.System.LogHandling.FileHandling;
using VHS.System.LogHandling.LogFileNameProvider;

namespace VHS.System.LogHandling
{
    public class LogInfoGatherer
    {
        private IFilesystemLayer _fsl;
        private LogFileGatherer _lfg;
        private LogFileLinesFileInfoConverter _il2fi;

        public LogInfoGatherer(FilesystemLayer.IFilesystemLayer fsl, ILogFileNameProvider _lfnp)
        {
            _fsl = fsl;
            _lfg = new LogFileGatherer(fsl, _lfnp);
            _il2fi = new LogFileLinesFileInfoConverter(new LogLinesToFileInfoConverter());
        }

        public List<FileInfoCollector.FileInformations> GetFileInfoLogFromPath(string path)
        {
            //TODO: unit test this
            var infoLines = _lfg.GetInfoLinesFromLog(path);
            return _il2fi.ConvertAll(infoLines);
        }
    }
}
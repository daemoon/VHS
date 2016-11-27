using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VHS.System
{
    public class VersionHandlingSystem
    {
        public VersionHandlingSystem(string path)
        {
            var fsl = new FilesystemLayer.FilesystemLayer();
            var infoGatherer = new FileInfoCollector(fsl);
            var currentFileInfo = infoGatherer.CollectFileInfos(path);
            var logGatherer = new LogInfoGatherer(fsl);
            var previousLog = logGatherer.GetFileInfoLogFromPath(path);
            var reporter = new Reporter();
            var report = reporter.CompareFileInfosAndCreateReport(currentFileInfo, previousLog);



            //Get old .VHS File
            //Compare files in report and new file list an

        }
    }

    public class Reporter
    {
        private IFileInfosComparer _fic;

        public Report CompareFileInfosAndCreateReport(ICollection<FileInfoCollector.FileInfo> currentFileInfo, List<FileInfoCollector.FileInfo> previousLog)
        {
            throw new global::System.NotImplementedException();
        }
    }

    public interface IFileInfosComparer
    {
        public EFileModification FindFilePairsAndCompare()
        {
            var pairedFileInfos = _fip.PairFileInfos();
            var comparedFileInfos = _fc.ComparePairedFileInfos(pairedFileInfos);

        }
    }

    public enum EFileModification
    {
        Modified,
        Removed,
        Added
    }

    public class FileInfosComparer : IFileInfosComparer
    {
        
    }



    public struct Report
    {
        public IEnumerable<string> Added;

        public IEnumerable<string> Removed;

        public IEnumerable<string> Modified;
    }
}

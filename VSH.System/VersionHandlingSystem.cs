using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VHS.System.FilesystemLayer;

namespace VHS.System
{
    public class VersionHandlingSystem
    {
        public VersionHandlingSystem(string path)
        {
            var fsl = new FilesystemLayer.FilesystemLayer();

            var currentFileInfo = new FileInfoCollector(fsl).CollectFileInfos(path);
            OutputReport report;
            try
            {
                var previousLog = new LogInfoGatherer(fsl).GetFileInfoLogFromPath(path);
                var pairedInfos = new InfoPairer(new PairCreator(new FilenamePicker()), new CompareFileInfoOnPath()).Pair(currentFileInfo, previousLog);
                var fileModificationsList = new FileModificationsListCreator(new ModificationClassificator()).CalculateFileModifications(pairedInfos);
                report = new FileModificationsReport(fileModificationsList,
                    new ModificationItemToStringConverterUsingToString());
            }
            catch (LfgLogFileDoesntExistException)
            {
                report = new NewDirectoryReport();
            }
            new LogInfoWriter(fsl).WriteLog(currentFileInfo, path);

        }
    }

    public class LogInfoWriter
    {
        private readonly IFilesystemLayer _fsl;
        private readonly LogFileLinesFileInfoConverter _fi2ll;

        public LogInfoWriter(IFilesystemLayer fsl)
        {
            _fsl = fsl;
        }

        public void WriteLog(ICollection<FileInfoCollector.FileInformations> fileInfo, string path)
        {
            _fsl.WriteFile(_fi2ll.RevertAll(fileInfo), path);
        }
    }


    public class FileModificationsListCreator
    {
        private IModificationClassificator _mc;

        public FileModificationsListCreator(IModificationClassificator mc)
        {
            _mc = mc;
        }


        public List<PerFileModification> CalculateFileModifications(List<InfoPair> pairedInfos)
        {
            var result = new List<PerFileModification>();
            foreach (var pair in pairedInfos)
            {
                result.Add(_mc.Classify(pair));
            }
            return result;

        }

    }

    public class FileModificationsReport : OutputReport
    {
        private List<PerFileModification> items;
        private IModificationItemToStringConverter _i2s;


        public FileModificationsReport(List<PerFileModification> items, IModificationItemToStringConverter i2S)
        {
            this.items = items;
            _i2s = i2S;
        }

        public override string Get()
        {
            if (items == null)
            {
                return "No change.";
            }
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendLine(_i2s.Convert(item));
            }
            return sb.ToString();
        }
    }

    public interface IModificationItemToStringConverter
    {
        string Convert(PerFileModification item);
    }

    public class ModificationItemToStringConverterUsingToString : IModificationItemToStringConverter
    {
        public string Convert(PerFileModification item)
        {
            return item.ToString();
        }
    }



    public abstract class OutputReport
    {
        public abstract string Get();
    }

    public class NewDirectoryReport : OutputReport
    {
        public override string Get()
        {
            return "New directory";
        }
    }

   
}

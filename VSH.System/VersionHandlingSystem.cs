using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VHS.System.FileInfoCollectors;
using VHS.System.FilesystemLayer.Exceptions;
using VHS.System.InfoPairing;
using VHS.System.InfoPairing.InfoComparers;
using VHS.System.InfoPairing.PairCreators;
using VHS.System.LogHandling;
using VHS.System.LogHandling.Converters;
using VHS.System.LogHandling.Converters.Single;
using VHS.System.LogHandling.Exceptions;
using VHS.System.LogHandling.FileHandling;
using VHS.System.LogHandling.LogFileNameProvider;
using VHS.System.ModificationsChecker.Classificators;
using VHS.System.ModificationsChecker.Converters;
using VHS.System.ModificationsChecker.ListCreators;
using VHS.System.ModificationsChecker.Removers;
using VHS.System.OutputReports;
using VHS.System.OutputReports.LineBreaker;

namespace VHS.System
{
    public class VersionHandlingSystem
    {
        private FilesystemLayer.FilesystemLayer _fsl;

        public VersionHandlingSystem()
        {

            _fsl = new FilesystemLayer.FilesystemLayer();
        }

        public OutputReport Run(string path)
        {
            OutputReport report;
            try
            {
                var logFileNameProvider = new DefaultLogFileNameProvider();
                var logFileRemover = new LogFileItemRemover(logFileNameProvider);
                var currentFileInfo = new FileInfoCollector(_fsl).CollectFileInfos(path);
                var currentFileInfoWithoutLogFile = logFileRemover.Remove(currentFileInfo);
                try
                {
                    var fileInfoFromLog = new LogInfoGatherer(_fsl, logFileNameProvider).GetFileInfoLogFromPath(path);
                    var fileInfoFromLogWithouLogFile = logFileRemover.Remove(fileInfoFromLog);
                    var pairedFileInfos =
                        new InfoPairer(new PairCreator(new FilenamePicker()), new CompareFileInfoOnPath()).Pair(
                            currentFileInfoWithoutLogFile, fileInfoFromLogWithouLogFile);
                    var fileModificationsList =
                        new FileModificationsListCreator(new ModificationClassificator()).CalculateFileModifications(
                            pairedFileInfos);
                    var fileModificationsListWithoutUnmodified = new UnmodifiedRemover().AlterList(fileModificationsList);
                    report = new FileModificationsReport(fileModificationsListWithoutUnmodified,
                        new ModificationItemToStringConverterUsingToString(), new HtmlLineBreaker());
                }
                catch (LfgLogFileDoesntExistException)
                {
                    report = new NewDirectoryReport();
                }
                new LogFileWriter(_fsl, new LogFileLinesFileInfoConverter(new LogLinesToFileInfoConverter()),
                    logFileNameProvider).WriteLog(currentFileInfoWithoutLogFile, path);
            }
            catch (Exception ex) when (ex is FilesystemLayerException)
            {
                report = new ErrorMessageReport(ex.Message);
            }
            return report;
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VHS.System.ModificationsChecker;

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
                        new ModificationItemToStringConverterUsingToString());
                }
                catch (LfgLogFileDoesntExistException)
                {
                    report = new NewDirectoryReport();
                }
                new LogFileWriter(_fsl, new LogFileLinesFileInfoConverter(new LogLinesToFileInfoConverter()),
                    logFileNameProvider).WriteLog(currentFileInfoWithoutLogFile, path);
            }
            catch (Exception ex)
            {
                report = new ErrorMessageReport(ex.Message);
            }
            return report;
        }
    }
}

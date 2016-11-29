using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace VHS.System.Tests
{
    [TestClass()]
    public class LogFileRemoverTests
    {
        [TestMethod()]
        public void RemoveTestShouldRemoveLogFile()
        {
            var lfn = new Mock<ILogFileNameProvider>();
            const string logFileName = "lfn";
            lfn.Setup(m => m.GetLogFileName()).Returns(logFileName);
            var lfr = new LogFileRemover(lfn.Object);
            var file1 = new FileInfoCollector.FileInformations() {FilePath = "file1"};
            var file2 = new FileInfoCollector.FileInformations() { FilePath = "file2" };
            var logFile = new FileInfoCollector.FileInformations() { FilePath = logFileName };
            var input = new List<FileInfoCollector.FileInformations>() {file1, file2, logFile};

            var result = lfr.Remove(input);

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(logFile) == false);
        }
    }


}
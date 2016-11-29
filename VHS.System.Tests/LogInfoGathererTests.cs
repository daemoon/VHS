using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VHS.System.FilesystemLayer;

namespace VHS.System.Tests
{
    [TestClass]
    public class LogFileGatherTest
    {
        [TestMethod]
        [ExpectedException(typeof(LfgLogFileDoesntExistException))]
        public void GetInfoLinesShouldThrowExceptionIfFileDoesntExists()
        {
            var fslMock = new Mock<IFilesystemLayer>();
            fslMock.Setup(m => m.GetStringLinesOfFile(It.IsAny<string>())).Throws(new FileSystemLayerFileNotFoundException());
            Mock<ILogFileNameProvider> lfnp = SetupLFNPMock();
            var lfg = new LogFileGatherer(fslMock.Object, lfnp.Object);

            var result = lfg.GetInfoLinesFromLog("dummy");
        }

        private static Mock<ILogFileNameProvider> SetupLFNPMock()
        {
            var lfnp = new Mock<ILogFileNameProvider>();
            lfnp.Setup(x => x.GetLogFileName()).Returns("abc");
            return lfnp;
        }

        [TestMethod]
        public void GetInfoLinesShouldReturnEmptyListIfInputIsNull()
        {
            var fslMock = new Mock<IFilesystemLayer>();
            fslMock.Setup(m => m.GetStringLinesOfFile(It.IsAny<string>())).Returns(() => null);
            Mock<ILogFileNameProvider> lfnp = SetupLFNPMock();
            var lfg = new LogFileGatherer(fslMock.Object, lfnp.Object);

            var result = lfg.GetInfoLinesFromLog("dummy");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetInfoLinesShouldReturnEmptyListIfInputIsEmpty()
        {
            var fslMock = new Mock<IFilesystemLayer>();
            fslMock.Setup(m => m.GetStringLinesOfFile(It.IsAny<string>())).Returns(new string[]{});
            Mock<ILogFileNameProvider> lfnp = SetupLFNPMock();
            var lfg = new LogFileGatherer(fslMock.Object, lfnp.Object);

            var result = lfg.GetInfoLinesFromLog("dummy");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetInfoLinesShouldConvertInputIntoList()
        {
            var fslMock = new Mock<IFilesystemLayer>();
            const string line1 = "line1";
            const string line2 = "line2";
            fslMock.Setup(m => m.GetStringLinesOfFile(It.IsAny<string>())).Returns(new string[] { line1, line2});
            Mock<ILogFileNameProvider> lfnp = SetupLFNPMock();
            var lfg = new LogFileGatherer(fslMock.Object, lfnp.Object);

            var result = lfg.GetInfoLinesFromLog("dummy");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result[0], line1);
            Assert.AreEqual(result[1], line2);
        }
    }
}

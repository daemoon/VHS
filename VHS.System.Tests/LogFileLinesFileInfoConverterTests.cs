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
    public class LogFileLinesFileInfoConverterTests
    {
        [TestMethod()]
        public void ConvertAllShouldReturnEmptyListOnEmptyInput()
        {
            var converterMock = new Mock<ILogLineToFileInfoConverter>();
            var expectedFileInfo = new FileInfoCollector.FileInformations() { FilePath = "dummy", Hash = "hash" };
            converterMock.Setup(m => m.Convert(It.IsAny<string>()))
                .Returns(expectedFileInfo);
            var converter = new LogFileLinesFileInfoConverter(converterMock.Object);
            var input = new List<string>() {};

            var result = converter.ConvertAll(input);

            Assert.IsTrue(result.Count == 0);
            Assert.IsInstanceOfType(result, typeof(List<FileInfoCollector.FileInformations>));


        }

        [TestMethod()]
        public void ConvertAllShouldProcessInput()
        {
            var converterMock = new Mock<ILogLineToFileInfoConverter>();
            var expectedFileInfo = new FileInfoCollector.FileInformations() {FilePath = "dummy", Hash = "hash"};
            converterMock.Setup(m => m.Convert(It.IsAny<string>()))
                .Returns(expectedFileInfo);
            var converter = new LogFileLinesFileInfoConverter(converterMock.Object);
            var input = new List<string>() {"1", "2", "3"};

            var result = converter.ConvertAll(input);

            Assert.IsTrue(result.Count == 3);
            Assert.AreEqual(expectedFileInfo,result[0]);


        }

        [TestMethod()]
        public void RevertAllShouldReturnEmptyListOnEmptyInput()
        {
            var converterMock = new Mock<ILogLineToFileInfoConverter>();
            const string expectedLines = "line";
            converterMock.Setup(m => m.Revert(It.IsAny<FileInfoCollector.FileInformations>()))
                .Returns(expectedLines);
            var converter = new LogFileLinesFileInfoConverter(converterMock.Object);
            var input = new List<FileInfoCollector.FileInformations>() { };

            var result = converter.RevertAll(input);

            Assert.IsTrue(result.Count == 0);
            Assert.IsInstanceOfType(result, typeof(List<string>));


        }

        [TestMethod()]
        public void RevertAllShouldProcessInput()
        {
            var converterMock = new Mock<ILogLineToFileInfoConverter>();
            var expectedFileInfo = new FileInfoCollector.FileInformations() { FilePath = "dummy", Hash = "hash" };
            converterMock.Setup(m => m.Convert(It.IsAny<string>()))
                .Returns(expectedFileInfo);
            var converter = new LogFileLinesFileInfoConverter(converterMock.Object);
            var input = new List<FileInfoCollector.FileInformations>() { new FileInfoCollector.FileInformations(), new FileInfoCollector.FileInformations() };

            var result = converter.RevertAll(input);

            Assert.IsTrue(result.Count == 2);


        }
    }
}
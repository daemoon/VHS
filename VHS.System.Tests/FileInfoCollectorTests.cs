using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using VHS.System.FilesystemLayer;

namespace VHS.System.Tests
{
    [TestClass()]
    public class FileInfoCollectorTests
    {
        [TestMethod()]
        public void ShouldReturnEmptyListOnEmptyFiles()
        {
            var fsl = new Mock<IFilesystemLayer>();
            var fnc = new Mock<IFilenamesCollector>();
            fnc.Setup(m => m.ReturnAllFilenamesInBasePathAndSubfolders(It.IsAny<string>()))
                .Returns(new List<string>());
            var hashProvider = new Mock<IContentHashingProvider>();
            hashProvider.Setup(m => m.GetContentOfFileAndHashIt(It.IsAny<string>())).Returns("123");
            var fic = new FileInfoCollector(fsl.Object, fnc.Object, hashProvider.Object);

            var result = fic.CollectFileInfos("dummyPath");

            Assert.IsInstanceOfType(result, typeof(List<FileInfoCollector.FileInformations>));
            Assert.IsTrue(result.Count == 0);

        }

        [TestMethod()]
        public void ShouldReturnAllFilesNamesAndDummyHash()
        {
            var fsl = new Mock<IFilesystemLayer>();
            var fnc = new Mock<IFilenamesCollector>();
            fnc.Setup(m => m.ReturnAllFilenamesInBasePathAndSubfolders(It.IsAny<string>()))
                .Returns(new List<string>() {"file1.txt", "file2.txt"});
            var hashProvider = new Mock<IContentHashingProvider>();
            hashProvider.Setup(m => m.GetContentOfFileAndHashIt(It.IsAny<string>())).Returns("123");
            var fic = new FileInfoCollector(fsl.Object, fnc.Object, hashProvider.Object);

            var result = fic.CollectFileInfos("dummyPath");

            Assert.IsInstanceOfType(result, typeof(List<FileInfoCollector.FileInformations>));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(new FileInfoCollector.FileInformations() {FilePath = "file1.txt", Hash = "123"}));
            Assert.IsTrue(result.Contains(new FileInfoCollector.FileInformations() { FilePath = "file2.txt", Hash = "123" }));

        }

    }
}
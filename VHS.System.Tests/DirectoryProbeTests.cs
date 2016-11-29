using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using VHS.System.FilenamesCollector;
using VHS.System.FilesystemLayer;

namespace VHS.System.Tests
{
    [TestClass()]
    public class DirectoryProbeTests
    {
        [TestMethod()]
        public void WorkIfDirectoryEmptyReturnEmptyListTest()
        {
            var fslMock = new Mock<IFilesystemLayer>();
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            var probe = new DirectoryProbe(string.Empty, fslMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 0);

        }

        [TestMethod()]
        public void WorkReturnsFilesInRootFolder()
        {
            const string filename1 = "file1.txt";
            const string filename2 = "file2.txt";
            var fslMock = new Mock<IFilesystemLayer>();
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>() { filename1, filename2 });
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            var probe = new DirectoryProbe(string.Empty, fslMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 2);
            Assert.AreEqual(result[0], filename1);
            Assert.AreEqual(result[1], filename2);
        }

        [TestMethod]
        public void WorkDoesntReturnSubfoldersInRootFolder()
        {
            const string dirname1 = "folder1";
            const string dirname2 = "folder2";
            var fslMock = new Mock<IFilesystemLayer>();            
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>() { dirname1, dirname2 });
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(dirname1)).Returns(new List<string>());
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(dirname1)).Returns(new List<string>());
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(dirname2)).Returns(new List<string>());
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(dirname2)).Returns(new List<string>());
            var probe = new DirectoryProbe(string.Empty, fslMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void WorkReturnOnlyFilesInRootFolder()
        {
            const string dirname = "folder1";
            const string filename1 = "file1.txt";
            const string filename2 = "file2.txt";
            var fslMock = new Mock<IFilesystemLayer>();
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>() {filename1, filename2});
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>() { dirname});
            fslMock.Setup(layer => layer.GetAllFilesInDirectory(dirname)).Returns(new List<string>());
            fslMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(dirname)).Returns(new List<string>());
            var probe = new DirectoryProbe(string.Empty, fslMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(filename1));
            Assert.IsTrue(result.Contains(filename2));
        }

        [TestMethod]
        public void WorkReturnsOnlyFilesInSubfolder()
        {
            const string subfolder = "subfolder";
            const string file1 = subfolder + "/file1.txt";
            const string file2 = subfolder + "/file2.txt";
            var flsMock = new Mock<IFilesystemLayer>();
            flsMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>() { subfolder });
            flsMock.Setup(layer => layer.GetAllFilesInDirectory(subfolder)).Returns(new List<string>() { file1, file2});
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(subfolder)).Returns(new List<string>());
            var probe = new DirectoryProbe(string.Empty, flsMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(file1));
            Assert.IsTrue(result.Contains(file2));
        }

        [TestMethod]
        public void WorkReturnsFilesInSubSubSubFolder()
        {
            const string subfolder = "subfolder";
            const string subsubfolder = "subfolder2";
            const string file1 = subfolder + "/"  + "file1.txt";
            const string file2 = subfolder + "/" + "file2.txt";
            var flsMock = new Mock<IFilesystemLayer>();

            flsMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>() { subfolder });
            flsMock.Setup(layer => layer.GetAllFilesInDirectory(subfolder)).Returns(new List<string>() { file1, file2 });
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(subfolder)).Returns(new List<string>() {subsubfolder});
            flsMock.Setup(layer => layer.GetAllFilesInDirectory(subsubfolder)).Returns(new List<string>());
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(subsubfolder)).Returns(new List<string>() );
            var probe = new DirectoryProbe(string.Empty, flsMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(file1));
            Assert.IsTrue(result.Contains(file2));
        }

        [TestMethod]
        public void WorkCorrectylConcatenatesThirdLevelFolderNamesFolder()
        {
            const string subfolder = "subfolder";
            const string subsubfolder = "subfolder2";
            const string subsubsubfolder = "subfolder3";
            const string file1 = subfolder+ "/" + subsubfolder + "/" + subsubsubfolder + "/" + "file1.txt";
            var flsMock = new Mock<IFilesystemLayer>();

            flsMock.Setup(layer => layer.GetAllFilesInDirectory(It.IsAny<string>())).Returns(new List<string>());
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(It.IsAny<string>())).Returns(new List<string>() { subfolder });
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(subfolder)).Returns(new List<string>() { subsubfolder });
            flsMock.Setup(layer => layer.GetAllFilesInDirectory(subsubfolder)).Returns(new List<string>());
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(subsubfolder)).Returns(new List<string>() {subsubsubfolder});
            flsMock.Setup(layer => layer.GetAllFilesInDirectory(subsubsubfolder)).Returns(new List<string>() {file1});
            flsMock.Setup(layer => layer.GetAllSubdirectoriesInDirectory(subsubsubfolder)).Returns(new List<string>());
            var probe = new DirectoryProbe(string.Empty, flsMock.Object);

            var result = probe.Work();

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Contains(file1));
        }
    }
}
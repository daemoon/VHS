using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System.FilesystemLayer;

namespace VHS.System.Tests
{
    [TestClass]
    public class FilesystemLayerTests
    {
        private readonly FilesystemLayer.FilesystemLayer _filesystemLayer;

        public FilesystemLayerTests()
        {
            _filesystemLayer = new FilesystemLayer.FilesystemLayer();
        }
        [TestMethod]
        [ExpectedException(typeof(CommonFileSystemLayerException))]
        public void GetAllSubirectoriesEmptyFilenameShouldThrowIncorrectNameException()
        {
            _filesystemLayer.GetAllSubdirectoriesInDirectory(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(CommonFileSystemLayerException))]
        public void GetAllSubirectoriesNullFilenameShouldThrowIncorrectNameException()
        {
            _filesystemLayer.GetAllSubdirectoriesInDirectory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(CommonFileSystemLayerException))]
        public void GetAllSubirectoriesInvalidCharsShouldThrowIncorrectNameException()
        {
            _filesystemLayer.GetAllSubdirectoriesInDirectory(@"<>");
        }

        [TestMethod]
        [ExpectedException(typeof(CommonFileSystemLayerException))]
        public void GetAllFilesEmptyFilenameShouldThrowIncorrectNameException()
        {
            _filesystemLayer.GetAllFilesInDirectory(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(CommonFileSystemLayerException))]
        public void GetAllFilesNullFilenameShouldThrowIncorrectNameException()
        {
            _filesystemLayer.GetAllFilesInDirectory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(CommonFileSystemLayerException))]
        public void GetAllFilesInvalidCharsShouldThrowIncorrectNameException()
        {
            _filesystemLayer.GetAllFilesInDirectory(@"<>");
        }
   
}
}

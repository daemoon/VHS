using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSH.System;
using VSH.System.Filesystem;

namespace VHS.System.Tests
{
    [TestClass]
    public class FilesystemLayerTests
    {
        [TestMethod]
        [ExpectedException(typeof(FilesystemLayerException))]
        public void GetAllSubirectoriesEmptyFilenameShouldThrowIncorrectNameException()
        {
            FilesystemLayer.GetAllSubdirectoriesInDirectory(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FilesystemLayerException))]
        public void GetAllSubirectoriesNullFilenameShouldThrowIncorrectNameException()
        {
            FilesystemLayer.GetAllSubdirectoriesInDirectory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FilesystemLayerException))]
        public void GetAllSubirectoriesInvalidCharsShouldThrowIncorrectNameException()
        {
            FilesystemLayer.GetAllSubdirectoriesInDirectory(@"<>");
        }

        [TestMethod]
        [ExpectedException(typeof(FilesystemLayerException))]
        public void GetAllFilesEmptyFilenameShouldThrowIncorrectNameException()
        {
            FilesystemLayer.GetAllFilesInDirectory(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FilesystemLayerException))]
        public void GetAllFilesNullFilenameShouldThrowIncorrectNameException()
        {
            FilesystemLayer.GetAllFilesInDirectory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FilesystemLayerException))]
        public void GetAllFilesInvalidCharsShouldThrowIncorrectNameException()
        {
            FilesystemLayer.GetAllFilesInDirectory(@"<>");
        }
   
}
}

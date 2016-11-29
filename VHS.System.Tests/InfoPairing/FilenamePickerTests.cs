using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHS.System.Tests
{
    [TestClass()]
    public class FilenamePickerTests
    {


        [TestMethod()]
        public void GetFileNameIfOnlyOldFileProvidedTakeFilenameFromItTest()
        {
            var fp = SetupFilePicker();
            const string path = "path";
            var input = new FileInfoCollector.FileInformations() { FilePath = path };

            var result = fp.GetFileName(null, input);

            Assert.AreEqual(path, result);
        }

        private static FilenamePicker SetupFilePicker()
        {
            return new FilenamePicker();
        }

        [TestMethod()]
        public void GetFileNameIfOnlyNewFileProvidedTakeFilenameFromItTest()
        {
            var fp = SetupFilePicker();
            const string path = "path";
            var input = new FileInfoCollector.FileInformations() { FilePath = path };

            var result = fp.GetFileName(input, null);

            Assert.AreEqual(path, result);
        }

        [TestMethod()]
        public void GetFileNameIfBothFilesProvidedAndAreSameTakeFromEither()
        {
            var fp = SetupFilePicker();
            const string path = "path";
            var oldFile = new FileInfoCollector.FileInformations() { FilePath = path };
            var newFile = new FileInfoCollector.FileInformations() { FilePath = path };

            var result = fp.GetFileName(newFile, oldFile);

            Assert.AreEqual(path, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(FilenamePickerException))]
        public void GetFileNameIfBothFilesProvidedAndAreDifferentThrowException()
        {
            var fp = SetupFilePicker();
            const string path = "path";
            const string differentPath = "differentPath";
            var oldFile = new FileInfoCollector.FileInformations() { FilePath = path };
            var newFile = new FileInfoCollector.FileInformations() { FilePath = differentPath };

            var result = fp.GetFileName(newFile, oldFile);
        }
    }
}
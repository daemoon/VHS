using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHS.System.FileInfoCollectors;
using VHS.System.LogHandling.Converters.Single;

namespace VHS.System.Tests
{
    [TestClass()]
    public class LogLineToFileInfoConverterTests
    {
        [TestMethod()]
        public void ConvertShouldCreateEqualFileInfo()
        {
            const string filePath = @"C:/Folder/File";
            const string hash = "abcd";
            var converter = new LogLinesToFileInfoConverter();
            var input = filePath + converter.Delimeter + hash;
            var expectedOutput = new FileInfoCollector.FileInformations() {FilePath = filePath, Hash = hash};

            var result = converter.Convert(input);

            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod()]
        public void RevertShouldCreateEqualFileInfo()
        {
            const string filePath = @"C:/Folder/File";
            const string hash = "abcd";
            var converter = new LogLinesToFileInfoConverter();
            var input = new FileInfoCollector.FileInformations() {FilePath = filePath, Hash = hash};
            var expectedOutput = filePath + converter.Delimeter + hash;

            var result = converter.Revert(input);

            Assert.AreEqual(expectedOutput, result);
        }
    }
}
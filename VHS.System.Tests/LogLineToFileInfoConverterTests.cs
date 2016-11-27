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
    public class LogLineToFileInfoConverterTests
    {
        [TestMethod()]
        public void ConvertShouldCreateEqualFileInfo()
        {
            const string filePath = @"C:/Folder/File";
            const string hash = "abcd";
            var converter = new LogLineToFileInfoConverter();
            var input = filePath + "|" + hash;
            var expectedOutput = new FileInfoCollector.FileInfo() {FilePath = filePath, Hash = hash};

            var result = converter.Convert(input);

            Assert.AreEqual(expectedOutput, result);
        }
    }
}
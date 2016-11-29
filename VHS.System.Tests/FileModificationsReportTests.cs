using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace VHS.System.Tests
{
    [TestClass()]
    public class FileModificationsReportTests
    {
        private const string converterOutput = "abc";

        [TestMethod()]
        public void ReportForEmptyListReturnMessage()
        {
            Mock<IModificationItemToStringConverter> i2s = I2SSetup();
            var report = new FileModificationsReport(null, i2s.Object);
            var expectedResult = "No change.";

            var result = report.ToString();

            Assert.AreEqual(expectedResult, result);


        }

        private static Mock<IModificationItemToStringConverter> I2SSetup()
        {
            var i2s = new Mock<IModificationItemToStringConverter>();
            i2s.Setup(m => m.Convert(It.IsAny<PerFileModification>())).Returns(converterOutput);
            return i2s;
        }

        [TestMethod]
        public void ReportForItemsReturnCorrectList()
        {
            Mock<IModificationItemToStringConverter> i2s = I2SSetup();
            List<PerFileModification> input = new List<PerFileModification>() {new PerFileModification(), new PerFileModification()};
            var report = new FileModificationsReport(input, i2s.Object);
            var expectedResult = converterOutput + Environment.NewLine + converterOutput + Environment.NewLine;

            var result = report.ToString();

            Assert.AreEqual(expectedResult, result);
        }
    }
}
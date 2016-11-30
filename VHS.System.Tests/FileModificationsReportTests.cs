using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VHS.System.ModificationsChecker.Converters;
using VHS.System.ModificationsChecker.Entity;
using VHS.System.OutputReports;
using VHS.System.OutputReports.LineBreaker;

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
            Mock<ILineBreaker> ilbMock = LineBreakerMock();
            var report = new FileModificationsReport(null, i2s.Object, ilbMock.Object);
            var expectedResult = "No change.";

            var result = report.Get();

            Assert.AreEqual(expectedResult, result);


        }

        private static Mock<ILineBreaker> LineBreakerMock()
        {
            var ilbMock = new Mock<ILineBreaker>();
            ilbMock.Setup(m => m.AddLineBreak(It.IsAny<string>())).Returns<string>(x => x);
            return ilbMock;
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
            Mock<ILineBreaker> ilbMock = LineBreakerMock();
            var report = new FileModificationsReport(input, i2s.Object, ilbMock.Object);
            var expectedResult = converterOutput + converterOutput;

            var result = report.Get();

            Assert.AreEqual(expectedResult, result);
        }
    }
}
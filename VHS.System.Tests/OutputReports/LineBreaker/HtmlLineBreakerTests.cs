using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System.OutputReports.LineBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHS.System.OutputReports.LineBreaker.Tests
{
    [TestClass()]
    public class HtmlLineBreakerTests
    {
        [TestMethod()]
        public void AddLineBreakAddsHtmlBreakAfterLineTest()
        {
            const string line = "line";
            const string expectedResult = line + "<br>";
            var lb = new HtmlLineBreaker();

            var result = lb.AddLineBreak(line);


            Assert.AreEqual(expectedResult, result);

        }
    }
}
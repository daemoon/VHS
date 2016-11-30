using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHS.System.FileInfoCollectors;
using VHS.System.InfoPairing.InfoComparers;

namespace VHS.System.Tests
{
    [TestClass()]
    public class CompareFileInfoOnPathTests
    {
        [TestMethod()]
        public void CompareFileInfoReturnsTrueIfPathsAreSame()
        {
            var comparer = new CompareFileInfoOnPath();
            const string path = "Path";
            var info1 = new FileInfoCollector.FileInformations() {FilePath = path, Hash = "random1"};
            var info2 = new FileInfoCollector.FileInformations() { FilePath = path, Hash = "random2" };
            var expectedResult = true;

            var result = comparer.CompareInfo(info1, info2);

            Assert.AreEqual(result, expectedResult);

        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using VHS.System.ModificationsChecker;

namespace VHS.System.Tests
{
    [TestClass()]
    public class FileModificationsListCreatorTests
    {

        [TestMethod()]
        public void CalculateFileModificationsReturnsEmptyListOnEmptyInput()
        {
            var mc = new Mock<IModificationClassificator>();
            mc.Setup(m => m.Classify(It.IsAny<InfoPair>())).Returns(new PerFileModification());
            var fmc = new FileModificationsListCreator(mc.Object);
            var input = new List<InfoPair>();

            var result = fmc.CalculateFileModifications(input);

            Assert.IsInstanceOfType(result, typeof(List<PerFileModification>));
            Assert.IsTrue(result.Count == 0);

        }
        [TestMethod()]
        public void CalculateFileModificationsProcessesEveryItemTest()
        {
            var mc = new Mock<IModificationClassificator>();
            mc.Setup(m => m.Classify(It.IsAny<InfoPair>())).Returns(new PerFileModification());
            var fmc = new FileModificationsListCreator(mc.Object);
            var input = new List<InfoPair>() {new InfoPair(), new InfoPair() };

            var result = fmc.CalculateFileModifications(input);

            Assert.IsInstanceOfType(result, typeof(List<PerFileModification>));
            Assert.IsTrue(result.Count == 2);
        }
    }
}
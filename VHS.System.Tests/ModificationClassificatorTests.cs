using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace VHS.System.Tests
{
    [TestClass()]
    public class ModificationClassificatorTests
    {
        [TestMethod()]
        public void ClassifyNoNewFileShouldReturnRemovedTest()
        {
            var mc = new ModificationClassificator();
            var input = new InfoPair() { NewFileHash = null, OldFileHash = "asdf" };

            var result = mc.Classify(input);

            Assert.IsTrue(result.Modification == EFileModification.Removed);
        }

        [TestMethod()]
        public void ClassifyNoOldFileShouldReturnAddedTest()
        {
            var mc = new ModificationClassificator();
            var input = new InfoPair() {NewFileHash = "asdf", OldFileHash = null};

            var result = mc.Classify(input);

            Assert.IsTrue(result.Modification == EFileModification.Added);
        }

        [TestMethod()]
        public void ClassifyBothFilesAndSameHashShouldReturnNoChange()
        {
            var mc = new ModificationClassificator();
            const string sameHash = "asdf";
            var input = new InfoPair() { NewFileHash = sameHash, OldFileHash = sameHash };

            var result = mc.Classify(input);

            Assert.IsTrue(result.Modification == EFileModification.NoChange);
        }

        [TestMethod()]
        public void ClassifyBothFilesAndDifferentHashShouldReturnNoChange()
        {
            var mc = new ModificationClassificator();
            const string hash = "asdf";
            const string differentHash = "fdsa";
            var input = new InfoPair() { NewFileHash = hash, OldFileHash = differentHash };

            var result = mc.Classify(input);

            Assert.IsTrue(result.Modification == EFileModification.Modified);
        }


    }
}
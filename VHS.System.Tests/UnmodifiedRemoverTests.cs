using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHS.System.ModificationsChecker;

namespace VHS.System.Tests
{
    [TestClass()]
    public class UnmodifiedRemoverTests
    {
        [TestMethod()]
        public void AlterListShouldRemoveItemsWithNotModifiedStatusTest()
        {
            var modificator = new UnmodifiedRemover();
            var addedFile = new PerFileModification() {Modification = EFileModification.Added};
            var removedFile = new PerFileModification() { Modification = EFileModification.Removed };
            var unchangedFile = new PerFileModification() { Modification = EFileModification.NoChange };
            var input = new List<PerFileModification>() {addedFile, removedFile, unchangedFile};

            var result = modificator.AlterList(input);

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(unchangedFile) == false);
        }
    }
}
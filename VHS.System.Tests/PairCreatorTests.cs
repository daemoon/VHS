using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using VHS.System.FileInfoCollectors;
using VHS.System.InfoPairing.PairCreators;

namespace VHS.System.Tests
{
    [TestClass()]
    public class PairCreatorTests
    {
        [TestMethod()]
        public void CreatePairShouldCreateInfoPairContainingBothInputs()
        {

            var fp =
                new Mock<IFilenamePicker>();
            fp.Setup(
                    m =>
                        m.GetFileName(It.IsAny<FileInfoCollector.FileInformations>(),
                            It.IsAny<FileInfoCollector.FileInformations>())).Returns("asdf");
            const string hash1 = "h";
            const string hash2 = "hash";
            var newFile = new FileInfoCollector.FileInformations() { FilePath = "fp1", Hash = hash1 };
            var oldFile = new FileInfoCollector.FileInformations() { FilePath = "fp2", Hash = hash2 };
            var pc = new PairCreator(fp.Object);

            var result = pc.CreatePair(newFile, oldFile);

            Assert.AreEqual(result.NewFileHash, hash1);
            Assert.AreEqual(result.OldFileHash, hash2);

        }
    }
}
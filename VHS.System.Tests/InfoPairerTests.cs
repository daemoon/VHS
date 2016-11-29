using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace VHS.System.Tests
{
    [TestClass()]
    public class InfoPairerTests
    {
        [TestMethod()]
        public void PairShouldReturnNewListIfEmptyInputs()
        {
            var pcMock = SetupBasicPairCreatorMock();
            var infoComparerMock = SetupBasicInfoComparerMock();
            var pairer = new InfoPairer(pcMock.Object, infoComparerMock.Object);
            List<FileInfoCollector.FileInformations> input1 = new List<FileInfoCollector.FileInformations>();
            List<FileInfoCollector.FileInformations> input2 = new List<FileInfoCollector.FileInformations>();

            var result = pairer.Pair(input1, input2);

            Assert.IsInstanceOfType(result, typeof(List<InfoPair>));
            Assert.IsTrue(result.Count == 0);

        }

        [TestMethod()]
        public void PairShouldCorrectlyPairLeftOnlyInput()
        {
            var pcMock = SetupBasicPairCreatorMock();
            Mock<IInfoComparer> infoComparerMock = SetupBasicInfoComparerMock();
            var pairer = new InfoPairer(pcMock.Object, infoComparerMock.Object);
            var fileInfo = new FileInfoCollector.FileInformations() { FilePath = "path", Hash = "hash" };
            List<FileInfoCollector.FileInformations> input1 = new List<FileInfoCollector.FileInformations>() { fileInfo };
            List<FileInfoCollector.FileInformations> input2 = new List<FileInfoCollector.FileInformations>();

            var result = pairer.Pair(input1, input2);

            Assert.IsInstanceOfType(result, typeof(List<InfoPair>));
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].NewFileHash, fileInfo.Hash);
            Assert.AreEqual(result[0].OldFileHash, null);


        }

  

        [TestMethod()]
        public void PairShouldCorrectlyPairRightOnlyInput()
        {
            var pcMock = SetupBasicPairCreatorMock();
            var infoComparerMock = SetupBasicInfoComparerMock();
            var pairer = new InfoPairer(pcMock.Object, infoComparerMock.Object);
            var fileInfo = new FileInfoCollector.FileInformations() { FilePath = "path", Hash = "hash" };
            List<FileInfoCollector.FileInformations> input1 = new List<FileInfoCollector.FileInformations>() ;
            List<FileInfoCollector.FileInformations> input2 = new List<FileInfoCollector.FileInformations>() { fileInfo };

            var result = pairer.Pair(input1, input2);

            Assert.IsInstanceOfType(result, typeof(List<InfoPair>));
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].OldFileHash, fileInfo.Hash);
            Assert.AreEqual(result[0].NewFileHash, null);


        }


        [TestMethod()]
        public void PairShouldCorrectlyMergeSameFileInfos()
        {
            var pcMock = SetupBasicPairCreatorMock();
            var infoComparerMock = SetupBasicInfoComparerMock();
            var pairer = new InfoPairer(pcMock.Object, infoComparerMock.Object);
            const string path = "path";
            var fileInfo1 = new FileInfoCollector.FileInformations() { FilePath = path, Hash = "hash1" };
            var fileInfo2 = new FileInfoCollector.FileInformations() { FilePath = path, Hash = "hash2" };
            List<FileInfoCollector.FileInformations> input1 = new List<FileInfoCollector.FileInformations>() { fileInfo1 };
            List<FileInfoCollector.FileInformations> input2 = new List<FileInfoCollector.FileInformations>() { fileInfo2 }; ;

            var result = pairer.Pair(input1, input2);

            Assert.IsInstanceOfType(result, typeof(List<InfoPair>));
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].NewFileHash, fileInfo1.Hash);
            Assert.AreEqual(result[0].OldFileHash, fileInfo2.Hash);


        }

        private static Mock<IInfoComparer> SetupBasicInfoComparerMock()
        {
            var infoComparerMock = new Mock<IInfoComparer>();
            infoComparerMock.Setup(
               m =>
                   m.CompareInfo(It.IsAny<FileInfoCollector.FileInformations>(),
                       It.IsAny<FileInfoCollector.FileInformations>())).Returns(true);
            return infoComparerMock;
        }


        private static Mock<IPairCreator> SetupBasicPairCreatorMock()
        {
            var pcMock = new Mock<IPairCreator>();
            pcMock.Setup(
                m =>
                    m.CreatePair(It.IsAny<FileInfoCollector.FileInformations?>(),
                        It.IsAny<FileInfoCollector.FileInformations?>())).Returns((
                            FileInfoCollector.FileInformations? x, FileInfoCollector.FileInformations? y) => new InfoPair() { FileName = "123", OldFileHash = y?.Hash, NewFileHash = x?.Hash});
            return pcMock;
        }
    }
}
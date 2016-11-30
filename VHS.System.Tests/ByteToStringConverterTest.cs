using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VHS.System.HashingProvider.Converters;

namespace VHS.System.Tests
{
    [TestClass]
    public class ByteToStringConverterTest
    {

        [TestMethod]
        public void ConvertNullInputShouldReturnEmptyString()
        {
            var btsc = new ByteToStringConverter();
            byte[] input = null;
            var expectedOutput = string.Empty;

            var convertedBytes = btsc.Convert(input);

            Assert.AreEqual(expectedOutput, convertedBytes);
        }


        [TestMethod]
        public void ConvertEmptyInputShouldReturnEmptyString()
        {
            var btsc = new ByteToStringConverter();
            var input = new byte[] {};
            var expectedOutput = string.Empty;

            var convertedBytes = btsc.Convert(input);

            Assert.AreEqual(expectedOutput, convertedBytes);
        }

        [TestMethod]
        public void ConvertShouldConvertBytesToHexRepresentation()
        {
            var btsc = new ByteToStringConverter();
            var input = new byte[]{0x11,0x22,0x33,0xFF};
            var expectedOutput = "112233ff";

            var convertedBytes = btsc.Convert(input);

            Assert.AreEqual(expectedOutput, convertedBytes);
        }

        
    }
    
}

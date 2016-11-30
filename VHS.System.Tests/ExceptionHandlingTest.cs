using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VHS.System.Tests
{
    [TestClass]
    public class ExceptionHandlingTest
    {
        public abstract class AbstractException : Exception
        {
        }

        public class DerivedException : AbstractException
        {
        }

        [TestMethod]
        public void AbstractExceptionShouldBeCoughtFromDerived()
        {
            try
            {
                throw new DerivedException();
            }
            catch (Exception ex) when (ex is AbstractException)
            {
                ;
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}

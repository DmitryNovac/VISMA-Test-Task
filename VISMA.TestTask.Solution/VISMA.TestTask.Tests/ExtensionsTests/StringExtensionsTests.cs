using Microsoft.VisualStudio.TestTools.UnitTesting;
using VISMA.TestTask.Core.Extensions;

namespace VISMA.TestTask.Tests.ExtensionsTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ToCapitalize_ShouldReturnValidString_WithSpaces()
        {
            var sourceStr = " firSt nAme";

            Assert.AreEqual("First Name", sourceStr.ToCapitalize());
        }

        [TestMethod]
        public void ToCapitalize_ShouldReturnValidString_WithoutSpaces()
        {
            var sourceStr = "FIRSTNAME ";

            Assert.AreEqual("Firstname", sourceStr.ToCapitalize());
        }

        [TestMethod]
        public void ToCapitalize_ShouldReturnValidString_WithoutHyphen()
        {
            var sourceStr = "firSt- nAme";

            Assert.AreEqual("First-Name", sourceStr.ToCapitalize());
        }
    }
}

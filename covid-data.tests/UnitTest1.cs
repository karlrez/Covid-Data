using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;

namespace covid_data.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetData_ShouldReturnAllData()
        {
            var controller = new CovidDataController();

            var result = controller.GetData();
            // 100 records plus heading
            Assert.AreEqual(101, result.Count);
        }
    }
}

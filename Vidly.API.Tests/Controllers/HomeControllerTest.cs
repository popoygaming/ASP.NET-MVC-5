using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vidly.API;
using Vidly.API.Controllers;

namespace Vidly.API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}

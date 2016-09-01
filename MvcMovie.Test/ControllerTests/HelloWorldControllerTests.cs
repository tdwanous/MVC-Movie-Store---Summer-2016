using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.Controllers;

namespace MvcMovie.Test.ControllerTests
{
    [TestClass]
    public class HelloWorldControllerTests : BaseControllerTest
    {
        private HelloWorldController _controllerUnderTest; 

        [TestMethod]
        public void HelloWorld_IndexTest_Success()
        {
            // Arrange
            _controllerUnderTest = new HelloWorldController();
            // Act
            var result = _controllerUnderTest.Index();
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void HelloWorld_WelcomeTest_Success()
        {
            // Arrange
            _controllerUnderTest = new HelloWorldController();
            var testString = "foo";
            var testNumTimes = 3;
            // Act
            var result = _controllerUnderTest.Welcome(testString, testNumTimes);
            // Assert 
            // We expect that a ViewResult object is returned
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult) result;
            // We expect that the ViewBag contains a Message and a NumTimes object
            Assert.IsFalse(string.IsNullOrEmpty(viewResult.ViewBag.Message));
            Assert.IsTrue(viewResult.ViewBag.NumTimes > 0);
            // We expect that the values for the objects contain those we passed in to Welcome
            Assert.IsTrue(viewResult.ViewBag.Message.Contains(testString));
            Assert.AreEqual(testNumTimes, viewResult.ViewBag.NumTimes);
            // Note: ViewBag is not something you will use frequently in production code.
        }
    }
}

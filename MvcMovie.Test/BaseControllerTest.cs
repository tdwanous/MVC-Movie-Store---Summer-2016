using System;
using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MvcMovie.Test
{
    public abstract class BaseControllerTest
    {
        Mock<HttpContextBase> _mockContextBase;
        Mock<HttpRequestBase> _mockRequestBase;
        Mock<HttpResponseBase> _mockResponseBase;

        [TestInitialize]
        public void Setup()
        {
            _mockContextBase = new Mock<HttpContextBase>();
            _mockRequestBase = new Mock<HttpRequestBase>();
            _mockResponseBase = new Mock<HttpResponseBase>();

            _mockContextBase.Setup(x => x.Request).Returns(_mockRequestBase.Object);
            _mockContextBase.Setup(x => x.Response).Returns(_mockResponseBase.Object);

            Stream writer = new MemoryStream();
            _mockResponseBase.SetupGet(ctx => ctx.OutputStream).Returns(writer);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockContextBase = null;
            _mockRequestBase = null;
            _mockResponseBase = null;
        }

    }
}

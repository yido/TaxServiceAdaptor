using NUnit.Framework;
using Moq;

namespace TaxServiceAdaptor.UnitTests
{
    public class HttpServiceTests
    {

        private Mock<IHttpService> _httpService;

        [SetUp]
        public void Setup()
        {
            _httpService = new Mock<IHttpService>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
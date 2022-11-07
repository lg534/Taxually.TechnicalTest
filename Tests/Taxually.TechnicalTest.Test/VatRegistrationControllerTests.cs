using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.DTOModel;

namespace Taxually.TechnicalTest.Test
{
    public class VatRegistrationControllerTests
    {
        private VatRegistrationRequest mRequestGB;
        private VatRegistrationRequest mRequestDE;
        private VatRegistrationRequest mRequestFR;

        private ITaxuallyQueueClient mQueueClient;
        private ITaxuallyHttpClient mHttpClient;
        private VatRegistrationController mController;

        [SetUp]
        public void Setup()
        {
            mRequestGB = new VatRegistrationRequest
            {
                Country = "GB",
                CompanyName = "test company1",
                CompanyId = "154241"
            };
            mRequestDE = new VatRegistrationRequest
            {
                Country = "DE",
                CompanyName = "test company2",
                CompanyId = "154242"
            };
            mRequestFR = new VatRegistrationRequest
            {
                Country = "FR",
                CompanyName = "test company3",
                CompanyId = "154243"
            };

            mQueueClient = Mock.Of<TaxuallyQueueClient>();
            Mock.Get(mQueueClient).Setup(x => x.EnqueueAsync(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(Task.CompletedTask);

            mHttpClient = Mock.Of<TaxuallyHttpClient>();
            Mock.Get(mHttpClient).Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<VatRegistrationRequest>())).Returns(Task.CompletedTask);

            mController = new VatRegistrationController();
        }

        [Test]
        public async Task VatRegistrationGBTest()
        {
            var result = await mController.Post(mRequestGB);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
        }

        [Test]
        public async Task VatRegistrationDETest()
        {
            var result = await mController.Post(mRequestDE);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
        }

        [Test]
        public async Task VatRegistrationFRTest()
        {
            var result = await mController.Post(mRequestFR);

            Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
        }

        [Test()]
        public void VatRegistrationFailTest()
        {
            var request = new VatRegistrationRequest
            {
                Country = "UK",
                CompanyName = "test company4",
                CompanyId = "154244"
            };

            Assert.ThrowsAsync<Exception>(async () => await mController.Post(request));
        }
    }
}
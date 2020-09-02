using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CoxExercise.Tests
{
    [TestClass]
    public class CoxApiTests
    {
        [TestMethod]
        public async Task GetDatasetId_Should_Return_Valid_Id()
        {
            //Setup
            var httpClientMoq = new Mock<ICoxHttpClient>();
            httpClientMoq.Setup(client => client.Get(It.IsAny<string>())).Returns(Task.FromResult("{\"datasetId\": \"123456\"}"));

            //Execute
            var datasetId=  await new CoxAPI(httpClientMoq.Object).GetDataSetId();
            Assert.AreEqual("123456", datasetId);
        }


        [TestMethod]
        public async Task GetDatasetId_Should_Throw_Exception_When_NoDatasetId_Is_Provided()
        {
            //Setup
            var httpClientMoq = new Mock<ICoxHttpClient>();
            httpClientMoq.Setup(client => client.Get(It.IsAny<string>())).Returns(Task.FromResult("{\"datasetId\": \"\"}"));

            //Execute
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await new CoxAPI(httpClientMoq.Object).GetDataSetId());
        }


        [TestMethod]
        public async Task GetDealer_Should_Return_Valid_Dealer()
        {
            //Setup
            var httpClientMoq = new Mock<ICoxHttpClient>();
            httpClientMoq.Setup(client => client.Get(It.IsAny<string>())).Returns(Task.FromResult("{\"dealerId\": 9,\"name\": \"Jimmy Baba\"}"));

            //Execute
            var dealer = await new CoxAPI(httpClientMoq.Object).GetDealer(It.IsAny<string>(), It.IsAny<int>());
            Assert.AreEqual("Jimmy Baba", dealer.Name);
        }
    }
}

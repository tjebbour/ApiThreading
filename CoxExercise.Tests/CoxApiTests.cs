using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CoxExercise.Tests
{
    [TestClass]
    public class CoxApiTests
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {

            var httpClientMoq = new Mock<ICoxHttpClient>();
            httpClientMoq.Setup(client => client.Get(It.IsAny<string>())).Returns(Task.FromResult("{\"datasetId\": \"0jP1ef9O2Ag\"}"));

            var api = new CoxAPI(httpClientMoq.Object);
            var ss=  await api.GetDataSetId();


        }
    }
}

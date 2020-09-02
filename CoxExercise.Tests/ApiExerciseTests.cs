using CoxExercise.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoxExercise.Tests
{
    [TestClass]
    public class ApiExerciseTests
    {
        [TestMethod]
        public async Task GetDatasetId_Should_Return_Valid_Id()
        {
            //Setup returns
            var datasetId = "112233";
            var vehiclesIds = new Vehicles() { VehicleIds = new List<int>() { 100, 101, 102 } };

            //setup dependencies

            //time tracker
            var timeTrackerMock = new Mock<ITimeTracker>();
            timeTrackerMock.Setup(p => p.Start());
            timeTrackerMock.Setup(p => p.Stop());

            //api mock
            var apiMock = new Mock<ICoxAPI>();
            apiMock.Setup(client => client.GetDataSetId()).Returns(Task.FromResult("112233"));
            apiMock.Setup(client => client.GetVehiclesIds(datasetId)).Returns(Task.FromResult(vehiclesIds));

            //Execute
            //var datasetId=  await new CoxAPI(apiMock.Object).GetDataSetId();
            //  Assert.AreEqual("123456", datasetId);
        }



        [TestMethod]
        public async Task GetDealer()
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

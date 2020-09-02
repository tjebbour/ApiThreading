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
        public async void GetDatasetId_Should_Return_Valid_Id()
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
            apiMock.Setup(client => client.GetVehiclesIds(datasetId)).Returns(Task.FromResult(new Vehicles() { VehicleIds = new List<int>() { 100, 101, 102 } }));
            apiMock.Setup(client => client.GetVehicle(It.IsAny<string>(),It.IsAny<int>())).Returns(Task.FromResult(new Vehicle() { DealerId = 888, Make = "Honda", Model = "Civic", VehicleId = 9637, Year = 1999 }));
            apiMock.Setup(client => client.GetDealer(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new Dealer() { DealerId = 888, Name = "Smix Chevy", Vehicles = new List<Vehicle>() { new Vehicle() { DealerId = 888, Make = "Honda", Model = "Civic", VehicleId = 9637, Year = 1999 } } }));
            apiMock.Setup(client => client.Save(It.IsAny<string>(), It.IsAny<Answer>())).Returns(Task.FromResult("{\"success\": true,\"message\": \"success!\",\"totalMilliseconds\": 45457}"));


            //Execute
            var result = await new ApiExercise(apiMock.Object, timeTrackerMock.Object).Run();
            Assert.AreEqual(true, result);
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

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
        public async Task Run_Should_Return_True()
        {
            //setup dependencies

            var timeTrackerMock = new Mock<ITimeTracker>();
            timeTrackerMock.Setup(p => p.Start());
            timeTrackerMock.Setup(p => p.Stop());

            //api mock
            var apiMock = new Mock<ICoxAPI>();
            apiMock.Setup(client => client.GetDataSetId()).Returns(Task.FromResult(It.IsAny<string>()));
            apiMock.Setup(client => client.GetVehiclesIds(It.IsAny<string>())).Returns(Task.FromResult(new Vehicles() { VehicleIds = new List<int>() { 100, 101, 102 } }));
            apiMock.Setup(client => client.GetVehicle(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new Vehicle() { DealerId = 888, Make = "Honda", Model = "Civic", VehicleId = 9637, Year = 1999 }));
            apiMock.Setup(client => client.GetDealer(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(new Dealer() { DealerId = 888, Name = "Smix Chevy", Vehicles = new List<Vehicle>() { new Vehicle() { DealerId = 888, Make = "Honda", Model = "Civic", VehicleId = 9637, Year = 1999 } } }));
            apiMock.Setup(client => client.Save(It.IsAny<string>(), It.IsAny<Answer>())).Returns(Task.FromResult("{\"success\": true,\"message\": \"success!\",\"totalMilliseconds\": 45457}"));

            //Execute
            var result = await new ApiExercise(apiMock.Object, timeTrackerMock.Object).Run();
            Assert.AreEqual(true, result);
        }
    }
}

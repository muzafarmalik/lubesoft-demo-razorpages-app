using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IRepos.IVehicle;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Common;
using DemoRazorPageApp.Models.Vehicle;
using DemoRazorPageApp.Services.Vehicle;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Tests.Vehicle
{
    [TestFixture]
    public class VehicleTests
    {
        #region Properties
        private string vehiclesJsonFilePath;
      

        #endregion
        #region Constructors

        public VehicleTests()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            vehiclesJsonFilePath = config["VehicleDataFilePath"];

        }
        #endregion

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public async Task TestAllVehicleLoadData()
        {
            //int vehiclesCount = 16;

            //var obj = new VehicleService();
            //var vehicles = (IEnumerable<VehicleModel>)obj.TestGetAllVehicles().Result?.Data;

            //Assert.IsTrue(vehicles.Count() == vehiclesCount);

            string path = System.IO.Directory.GetCurrentDirectory() + "\\data\\vehicles.json";

            var mockVehicleService = new Mock<IVehicleService>();
            var mockAppSettings = new Mock<IAppSettings>();
            var mockVehicleRepo = new Mock<IVehicleRepo>();
           
            BaseResponse getResponse = new BaseResponse();

            mockVehicleService.Setup(x => x.TestGetAllVehiclesForUnitTest(path)).ReturnsAsync(getResponse);

          
            var vehicleService = new VehicleService(mockAppSettings.Object, mockVehicleRepo.Object);

            var response = await vehicleService.TestGetAllVehiclesForUnitTest(path);

            Assert.IsInstanceOf<BaseResponse>(response);
            var result = response as BaseResponse;
            Assert.IsInstanceOf<IEnumerable<VehicleModel>>(result.Data);
            var vehicles = (IEnumerable<VehicleModel>)result.Data;
            Assert.IsTrue(vehicles.Count() == 16);
        }


    }
}

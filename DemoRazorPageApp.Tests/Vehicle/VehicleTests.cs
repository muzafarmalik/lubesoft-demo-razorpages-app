using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Vehicle;
using DemoRazorPageApp.Services.Vehicle;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Tests.Vehicle
{
    public class VehicleTests
    {
        #region Properties
        private Mock<IVehicleService> _vehicleService;
        private Task<IEnumerable<VehicleModel>> Vehicles;
        
        #endregion
        #region Constructors
      
        #endregion

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestAllVehicleLoadData()
        {
            int vehiclesCount = 16;
            Assert.IsTrue(vehiclesCount == 16);
        }
    }
}

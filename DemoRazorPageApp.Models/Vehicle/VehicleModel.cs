using DemoRazorPageApp.Models.Common;
using System;
using System.Collections.Generic;

namespace DemoRazorPageApp.Models.Vehicle
{
    public class VehicleModel : BaseModel
    {
        public string VehicleId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string VehicleDescription { get; set; }
        public string VIN { get; set; }
        public string LastServiceDate { get; set; }
    }

    public class VehicleJsonModel
    {
        public List<VehicleModel> Vehicles { get; set; }
    }
}

using DemoRazorPageApp.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Models.Vehicle
{
    public class VehicleListModel : BaseModel
    {
        public string VehicleId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string VehicleDescription { get; set; }
        public string VIN { get; set; }
        public string LastServiceDate { get; set; }
    }

    public class VehicleListJsonModel
    {
        public List<VehicleListModel> Vehicles { get; set; }
    }
}

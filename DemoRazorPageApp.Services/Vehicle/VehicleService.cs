using DemoRazorPageApp.Common.DataHelpers;
using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IRepos.IVehicle;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Common;
using DemoRazorPageApp.Models.Vehicle;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace DemoRazorPageApp.Services.Vehicle
{
    public class VehicleService: IVehicleService
    {
        #region Properties
        private readonly IAppSettings _appSettings;
        private readonly IVehicleRepo _vehicleRepo;

        #endregion

        #region Constructor

        public VehicleService(IAppSettings appSettings, IVehicleRepo vehicleRepo)
        {
            _appSettings = appSettings;
            _vehicleRepo = vehicleRepo;
        }

        #endregion

        #region Implementations
        public async Task<BaseResponse> GetAllVehicles(HttpRequest request)
        {
            IEnumerable<VehicleModel> vehicles;

            using (StreamReader r = new StreamReader(_appSettings.VehicleDataFilePath))
            {
                string json = r.ReadToEnd();
                vehicles = JsonConvert.DeserializeObject<VehicleJsonModel>(json).Vehicles;

                //ToDO

                //No need to transform if model is all same as binded model
                //Or Use AutoMapper and Configs
                //vehicles = await TransformIntoViewModel(vehiclesModel.Vehicles);
            }

            DataTableRequest model = DataService.TransformIntoModel(request);

            //Sorting    
            if (!(string.IsNullOrEmpty(model.SortColumn) && string.IsNullOrEmpty(model.SortColumnDir)))
            {
                vehicles = vehicles.AsQueryable().OrderBy(model.SortColumn + " " + model.SortColumnDir);
            }

            // Searching
            var searchValue = model.SearchString;

            if (!string.IsNullOrEmpty(searchValue))
            {
                vehicles = vehicles.Where(x => x.VehicleId.ToLower().Contains(searchValue) || (x.CustomerName != null && x.CustomerName.ToLower().Contains(searchValue)) ||
                                         (x.VehicleDescription != null && x.VehicleDescription.ToLower().Contains(searchValue)) ||
                                         (x.VIN != null && x.VIN.ToLower().Contains(searchValue)));
            }

            int recordsTotal = vehicles.Count();

            BaseListResponse listResponse = new BaseListResponse
            {
                Draw = model.Draw,
                RecordsFiltered = vehicles.Count(),
                RecordsTotal = recordsTotal,
                Data = vehicles.Skip(model.Skip).Take(model.PageSize).ToList()
            };

            return DataService.Response(null, listResponse);

        }

        public int TestGetAllVehicles()
        {
            IEnumerable<VehicleModel> vehicles;

            using (StreamReader r = new StreamReader(_appSettings.VehicleDataFilePath))
            {
                string json = r.ReadToEnd();
                vehicles = JsonConvert.DeserializeObject<VehicleJsonModel>(json).Vehicles;

                //ToDO

                //No need to transform if model is all same as binded model
                //Or Use AutoMapper and Configs
                //vehicles = await TransformIntoViewModel(vehiclesModel.Vehicles);
            }

            return vehicles.Count();
        }
        #endregion

        #region Private Helpers

        private async Task<IEnumerable<VehicleModel>> TransformIntoViewModel(List<VehicleModel> vehicles)
        {
            List<VehicleModel> vehicleModelObj = new List<VehicleModel>();

            foreach (var vehicle in vehicles)
            {
                vehicleModelObj.Add(new VehicleModel()
                {
                    Id = vehicle.Id,
                    VehicleId = vehicle.VehicleId,
                    CustomerName = vehicle.CustomerName,
                    VehicleDescription = vehicle.VehicleDescription,
                    Phone = vehicle.Phone,
                    VIN = vehicle.VIN,
                    LastServiceDate = vehicle.LastServiceDate
                });
            }

            return vehicleModelObj;
        }
        #endregion
    }
}

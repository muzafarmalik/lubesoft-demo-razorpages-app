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
using System;

namespace DemoRazorPageApp.Services.Vehicle
{
    public class VehicleService: IVehicleService
    {
        #region Properties
        private readonly IAppSettings _appSettings;
        private readonly IVehicleRepo _vehicleRepo;

        public VehicleService()
        {
        }

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
            var vehicles = await GetTransformedVehicleListModels();

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
        public async Task<BaseResponse> GetVehicleById(int vehicleId)
        {
            var vehicles = await ReadVehicleJsonDataFile();
            var vehicle = vehicles.Where(x => x.Id == vehicleId)?.FirstOrDefault();
            return DataService.Response(null, vehicle);
        }

        public async Task<BaseResponse> UpdateVehicle(VehicleModel vehicleModel)
        {
            var vehicleUpdatedObject = new VehicleListModel
            {
                Id = vehicleModel.Id,
                VehicleId = vehicleModel.VehicleId,
                CustomerName = vehicleModel.CustomerName,
                VehicleDescription = vehicleModel.VehicleDescription,
                Phone = vehicleModel.Phone,
                VIN = vehicleModel.VIN,
                LastServiceDate = vehicleModel.LastServiceDate.ToString("yyyy/MM/dd")
            };
            //Loading Existing data file

            IEnumerable<VehicleListModel> vehicleListModel;

            vehicleListModel = await GetTransformedVehicleListModels();

            var allExistingVehicles = vehicleListModel.ToList();
            var vehicleToUpdate = allExistingVehicles.Where(x => x.Id == vehicleUpdatedObject.Id).FirstOrDefault();

            allExistingVehicles.Remove(vehicleToUpdate);

            allExistingVehicles.Add(vehicleUpdatedObject);

         
            if (allExistingVehicles != null && allExistingVehicles.Any())
            {
                System.IO.File.Delete(_appSettings.VehicleDataFilePath);

                var vehicleWriteModel = new VehicleListJsonModel { Vehicles = allExistingVehicles };
                var jsonFile = Utility.WriteJson(vehicleWriteModel, _appSettings.VehicleDataFilePath);

            }
            //Reloading new file - json data of vehicles
            vehicleListModel = await GetTransformedVehicleListModels();



            return DataService.Response(null, vehicleListModel);
        }
        public async Task<BaseResponse> TestGetAllVehicles(string vehiclesJsonFilePath)
        {
            var vehicles = await ReadVehicleJsonDataFile();
            return DataService.Response(null, vehicles);
        }
        public async Task<BaseResponse> TestGetAllVehiclesForUnitTest(string vehiclesJsonFilePath)
        {
            var vehicles = await TestReadVehicleJsonDataFile(vehiclesJsonFilePath);
            return DataService.Response(null, vehicles);
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
        private async Task<IEnumerable<VehicleModel>> ReadVehicleJsonDataFile()
        {
            IEnumerable<VehicleModel> vehicles;

            using (StreamReader r = new StreamReader(_appSettings.VehicleDataFilePath))
            {
                string json = r.ReadToEnd();
                vehicles = JsonConvert.DeserializeObject<VehicleJsonModel>(json)?.Vehicles;

                //ToDO

                //No need to transform if model is all same as binded model
                //Or Use AutoMapper and Configs
                //vehicles = await TransformIntoViewModel(vehiclesModel.Vehicles);
            }
            return vehicles;
        }
        private async Task<IEnumerable<VehicleModel>> TestReadVehicleJsonDataFile(string vehiclesJsonFilePath)
        {
            IEnumerable<VehicleModel> vehicles;

            using (StreamReader r = new StreamReader(vehiclesJsonFilePath))
            {
                string json = r.ReadToEnd();
                vehicles = JsonConvert.DeserializeObject<VehicleJsonModel>(json).Vehicles;

                //ToDO

                //No need to transform if model is all same as binded model
                //Or Use AutoMapper and Configs
                //vehicles = await TransformIntoViewModel(vehiclesModel.Vehicles);
            }
            return vehicles;
        }
        private async Task<IEnumerable<VehicleListModel>> GetTransformedVehicleListModels()
        {
            var vehiclesList = await ReadVehicleJsonDataFile();

            return vehiclesList.Select(x => new VehicleListModel
            {
                Id = x.Id,
                VehicleId = x.VehicleId,
                CustomerName = x.CustomerName,
                VehicleDescription = x.VehicleDescription,
                Phone = x.Phone,
                VIN = x.VIN,
                LastServiceDate = x.LastServiceDate.ToString("yyyy/MM/dd")
            });
        }
        #endregion
    }
}

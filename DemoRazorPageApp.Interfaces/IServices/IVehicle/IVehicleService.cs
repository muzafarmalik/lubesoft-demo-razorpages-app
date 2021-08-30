using DemoRazorPageApp.Models.Common;
using DemoRazorPageApp.Models.Vehicle;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Interfaces.IServices.IVehicle
{
    public interface IVehicleService
    {
        Task<BaseResponse> GetAllVehicles(HttpRequest request);
        Task<BaseResponse> TestGetAllVehicles(string vehiclesJsonFilePath);
        Task<BaseResponse> TestGetAllVehiclesForUnitTest(string vehiclesJsonFilePath);
        Task<BaseResponse> GetVehicleById(int vehicleId);
        Task<BaseResponse> UpdateVehicle(VehicleModel vehicleModel);
    }
}

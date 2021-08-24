using DemoRazorPageApp.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Interfaces.IServices.IVehicle
{
    public interface IVehicleService
    {
        Task<BaseResponse> GetAllVehicles(HttpRequest request);
    }
}

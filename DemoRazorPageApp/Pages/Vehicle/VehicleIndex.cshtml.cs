using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Pages.Vehicle
{
    [IgnoreAntiforgeryToken]
    public class VehicleIndexModel : PageModel
    {
        #region Properties
        private readonly IAppSettings _appSettings;
        private readonly ILogger<VehicleIndexModel> _logger;
        private readonly IVehicleService _vehicleService;

        #endregion

        #region Constructors
        public VehicleIndexModel(ILogger<VehicleIndexModel> logger, IAppSettings appSettings, 
                                 IVehicleService vehicleService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _vehicleService = vehicleService;
        }
        #endregion

        #region Implentations
        public void OnGet()
        {

        }

        public async Task<JsonResult> OnPostData()
        {
            BaseResponse response = await _vehicleService.GetAllVehicles(Request);
            return new JsonResult(response.Data);
        }

        #endregion

        #region Private Methods

       
        #endregion
    }
}

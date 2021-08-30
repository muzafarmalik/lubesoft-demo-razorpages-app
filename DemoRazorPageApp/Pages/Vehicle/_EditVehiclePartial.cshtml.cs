using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoRazorPageApp.Common.DataHelpers;
using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Common;
using DemoRazorPageApp.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DemoRazorPageApp.Pages.Vehicle
{
    public class _EditVehiclePartialModel : PageModel
    {
        #region Properties
      
        private readonly IAppSettings _appSettings;
        private readonly ILogger<_EditVehiclePartialModel> _logger;
        private readonly IVehicleService _vehicleService;


        public VehicleModel Model;

        #endregion

        #region Constructors
        public _EditVehiclePartialModel(ILogger<_EditVehiclePartialModel> logger, IAppSettings appSettings,
                                 IVehicleService vehicleService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _vehicleService = vehicleService;
        }
        #endregion

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostVehicleUpdate(VehicleModel vehicleModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response =  await _vehicleService.UpdateVehicle(vehicleModel);
            }

            catch (Exception ex)
            {
                return new JsonResult(ex?.Message);
            }

            return new JsonResult(response.Data);
        }
       
        public async Task<IActionResult> OnGetVehiclePartial(int vehicleId)
        {
            var vehicle = (VehicleModel)_vehicleService.GetVehicleById(vehicleId).Result.Data;

            var model = new _EditVehiclePartialModel(_logger, _appSettings, _vehicleService)
            {
                Model = new VehicleModel
                {
                    Id = vehicle.Id,
                    VehicleId = vehicle.VehicleId,
                    CustomerName = vehicle.CustomerName,
                    Phone = vehicle.Phone,
                    VehicleDescription = vehicle.VehicleDescription,
                    VIN = vehicle.VIN,
                    LastServiceDate = vehicle.LastServiceDate

                }
            };

            Model = model?.Model;

            return Page();
        }

        #region Private Methods

       
        #endregion
    }
}

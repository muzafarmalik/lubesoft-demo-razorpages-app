using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Common;
using DemoRazorPageApp.Models.Vehicle;
using DemoRazorPageApp.Pages.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        #region Properties
        private readonly IAppSettings _appSettings;
        private readonly ILogger<IndexModel> _logger;
        private readonly IVehicleService _vehicleService;

        public _EditVehiclePartialModel Model { get; set; }
        #endregion

        #region Constructors
        public IndexModel(ILogger<IndexModel> logger, IAppSettings appSettings,
                                 IVehicleService vehicleService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _vehicleService = vehicleService;
        }

        #endregion

        #region Implentations

        //public async Task<IActionResult> OnPost()
        //{
        //    var vehicleId = Request.Form["vehicleId"];
        //    BaseResponse response = await _vehicleService.GetAllVehicles(Request);
        //    return new JsonResult(response.Data);
        //}
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

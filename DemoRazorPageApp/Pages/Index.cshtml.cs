using DemoRazorPageApp.Interfaces.ICommon;
using DemoRazorPageApp.Interfaces.IServices.IVehicle;
using DemoRazorPageApp.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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

        #endregion

        #region Constructors
        public IndexModel(ILogger<IndexModel> logger, IAppSettings appSettings, IVehicleService vehicleService)
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

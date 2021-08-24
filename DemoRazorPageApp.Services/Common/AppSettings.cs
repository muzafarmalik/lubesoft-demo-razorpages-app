using DemoRazorPageApp.Interfaces.ICommon;

namespace DemoRazorPageApp.Services.Common
{
    public class AppSettings : IAppSettings
    {
        public string VehicleDataFilePath { get; set; }
      
    }
}

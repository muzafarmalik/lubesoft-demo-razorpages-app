namespace DemoRazorPageApp.Interfaces.ICommon
{
    public interface IAppSettings
    {
        public string VehicleDataFilePath { get; set; }
        public string VehicleDataFileBackupPath { get; set; }
    }
}

namespace DemoRazorPageApp.Models.Common
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }

    public class BaseListResponse
    {
        public string Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public object Data { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Models.Common
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}

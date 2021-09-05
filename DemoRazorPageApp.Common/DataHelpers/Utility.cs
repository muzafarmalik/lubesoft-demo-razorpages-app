using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoRazorPageApp.Common.DataHelpers
{
    public static class Utility
    {
        public static async Task<string> WriteJson(object listObj ,string path)
        {
            //using FileStream createStream = File.Create(@"D:\path.json");
            using FileStream createStream = File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, listObj);

            return createStream.Name;
        }

    }
}

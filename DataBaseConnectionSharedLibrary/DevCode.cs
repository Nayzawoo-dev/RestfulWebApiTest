using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataBaseConnectionSharedLibrary
{
    public static class DevCode
    {
        public static bool IsNullOrEmpty1(this string? str)
        {
            var res = str == null || string.IsNullOrEmpty(str.Trim()) || string.IsNullOrWhiteSpace(str.Trim());
            return res;
        }

        public static string ToJson1(this object obj)
        {
            var res = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return res;
        }
    }

   
}

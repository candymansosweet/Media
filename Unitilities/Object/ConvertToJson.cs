using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Unitilities.Object
{
    public class ConvertToJson
    {
        public static string ConvertObjectToJson(object obj)
        {
            // Tùy chọn cho format đẹp hơn (pretty printing)
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Tạo ra JSON dễ đọc hơn với format đẹp
            };

            // Chuyển đổi object sang chuỗi JSON
            return JsonSerializer.Serialize(obj, options);
        }
    }
}

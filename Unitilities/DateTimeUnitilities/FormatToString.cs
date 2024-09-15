using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitilities.DateTimeUnitilities;

public class FormatToString
{
    /// <summary>
    /// trả về dateTime chuyển thành string để tạo tên folder/file
    /// </summary>
    /// <param name="dateTime">thời gian muốn convert</param>
    /// <param name="format">format muốn lây</param>
    /// <returns></returns>
    static public string FormatToPath(DateTime dateTime, string format)
    {
        // Chuyển đổi DateTime sang chuỗi theo format "dd/MM/yyyy"
        string formattedDate = dateTime.ToString(format);

        // Thay thế ký tự "/" thành "_"
        formattedDate = formattedDate.Replace("/", "_");

        return formattedDate;
    }
    static public string FormatToPath(string format)
    {
        // Chuyển đổi DateTime sang chuỗi theo format "dd/MM/yyyy"
        string formattedDate = DateTime.UtcNow.Date.ToString(format);

        // Thay thế ký tự "/" thành "_"
        formattedDate = formattedDate.Replace("/", "_");

        return formattedDate;
    }
}






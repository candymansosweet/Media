using System.Globalization;
namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public ExceptionCode Code { get; } // mã lỗi

        public IEnumerable<string> Errors { get; } // tập thông điệp lỗi
        public IEnumerable<ErrorDetail> ErrorDetails { get; }

        public AppException() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
        public AppException(ExceptionCode code, string message, IEnumerable<ErrorDetail>? errorDetails = null)
        {
            Code = code;
            Errors = new List<string>() { message };
            ErrorDetails = errorDetails ?? new List<ErrorDetail>();
        }


    }
    public enum ExceptionCode
    {
        Invalidate = 1, // giá trị không hợp lệ
        Notfound = 2, // không tìm thấy
        Duplicate = 3, // trùng lặp
        //Duplicate = 4, // 
    }
    public class ErrorDetail
    {
        public string Key { get; }
        public dynamic Value { get; }

        public ErrorDetail(string key, dynamic value)
        {
            Key = key;
            Value = value;
        }
    }

}
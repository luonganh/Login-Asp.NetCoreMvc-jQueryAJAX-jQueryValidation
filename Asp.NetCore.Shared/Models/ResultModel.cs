namespace Asp.NetCore.Shared.Models
{
    public class ResultModel
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public int? Code { get; set; }
        public object? Data { get; set; }
    }

    public class ResultModel<T>
    {
        public ResultModel()
        {

        }

        public ResultModel(T data)
        {
            Data = data;
        }

        public ResultModel(bool? success, int code, T data)
        {
            Success = success;
            Code = code;
            Data = data;
        }

        public ResultModel(bool? success, int code, string message)
        {
            Success = success;
            Code = code;
            Message = message;
        }

        public ResultModel(bool? success, int code, string message, T data)
        {
            Success = success;
            Code = code;
            Message = message;
            Data = data;
        }
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public int? Code { get; set; }
        public T Data { get; set; }
    }
}

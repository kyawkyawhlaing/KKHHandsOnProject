namespace KKHHandsOnProject.BlogMvcApp.src.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
        public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }
        public bool IsNotFound { get { return Type == EnumRespType.NotFound; } }
        private EnumRespType Type { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public static Result<T> Success(T? data = default, string message = "Success")
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Data = data,
                Message = message
            };
        }
        public static Result<T> NotFound(string message, T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumRespType.NotFound,
                Data = data,
                Message = message
            };
        }
        public static Result<T> ValidationError(string message = "Success", T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumRespType.ValidationError,
                Data = data,
                Message = message
            };
        }
        public static Result<T> SystemError(string message = "Success", T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumRespType.SystemError,
                Data = data,
                Message = message
            };
        }

    }

    public enum EnumRespType
    {
        None,
        NotFound,
        Success,
        ValidationError,
        SystemError
    }
}

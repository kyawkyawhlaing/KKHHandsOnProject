namespace KKHHandsOnProject.Shared.Models;

public class Result<T>
{
         public bool IsSuccess { get; set; }
         public bool IsError => !IsSuccess;
         public bool IsValidationError => ResultType == EnumResultType.ValidationError;
         public bool IsSystemError => ResultType == EnumResultType.SystemError;
         public bool IsNotFound => ResultType == EnumResultType.NotFound;
         private EnumResultType ResultType { get; set; }
         public T? Data { get; set; }
         public string? Message { get; set; }
         public static Result<T> Success(T? data = default, string message = "Success")
         {
             return new Result<T>
             {
                 IsSuccess = true,
                 ResultType = EnumResultType.Success,
                 Data = data,
                 Message = message
             };
         }
         public static Result<T> NotFound(string message, T? data = default)
         {
             return new Result<T>
             {
                 IsSuccess = false,
                 ResultType = EnumResultType.NotFound,
                 Data = data,
                 Message = message
             };
         }
         public static Result<T> ValidationError(string message = "Success", T? data = default)
         {
             return new Result<T>
             {
                 IsSuccess = true,
                 ResultType = EnumResultType.ValidationError,
                 Data = data,
                 Message = message
             };
         }
         public static Result<T> SystemError(string message = "Success", T? data = default)
         {
             return new Result<T>
             {
                 IsSuccess = true,
                 ResultType = EnumResultType.SystemError,
                 Data = data,
                 Message = message
             };
         }   
}
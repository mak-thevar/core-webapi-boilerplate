using System.Net;

namespace CoreWebApiBoilerPlate.Application.DTO
{
    public class ApiResponseModel<T>
    {
        // Default Constructor
        public ApiResponseModel()
        {
            Succeeded = false;
            Result = default;
            Errors = new List<string>();
            StatusCode = HttpStatusCode.InternalServerError;
        }

        // Public Parameterized Constructor
        public ApiResponseModel(bool succeeded, T? result, IEnumerable<string> errors, HttpStatusCode statusCode)
        {
            Succeeded = succeeded;
            Result = result;
            Errors = errors ?? new List<string>();
            StatusCode = statusCode;
        }

        // Public Properties
        public bool Succeeded { get; set; }
        public T? Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Status => StatusCode.ToString();

        // Success Factory Method
        public static ApiResponseModel<T> Success(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponseModel<T>(true, result, new List<string>(), statusCode);
        }

        // Failure Factory Method
        public static ApiResponseModel<T> Failure(IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponseModel<T>(false, default, errors, statusCode);
        }

        // Failure with a single error message
        public static ApiResponseModel<T> Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponseModel<T>(false, default, new List<string> { errorMessage }, statusCode);
        }
    }

}

using System.Net;

namespace CoreWebApiBoilerPlate.BusinessLogicLayer.DTO
{
    public class ApiResponseModel<T>
    {
        public ApiResponseModel() { }

        private ApiResponseModel(bool succeeded, T? result, IEnumerable<string> errors, HttpStatusCode statusCode)
        {
            Succeeded = succeeded;
            Result = result;
            Errors = errors;
        }

        public bool Succeeded { get; set; }

        public T? Result { get; set; }

        public IEnumerable<string>? Errors { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Status { get => StatusCode.ToString(); }

        public static ApiResponseModel<T> Success(T result)
        {
            return new ApiResponseModel<T>(true, result, new List<string>(), HttpStatusCode.OK);
        }

        public static ApiResponseModel<T> Failure(IEnumerable<string> errors)
        {
            return new ApiResponseModel<T>(false, default, errors, HttpStatusCode.BadRequest);
        }
    }
}

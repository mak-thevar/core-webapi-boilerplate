using System.Collections.Generic;

namespace CoreWebApiBoilerPlate.Models
{
    public class DefaultResponseModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<Errors> Errors { get; set; } = new List<Errors>();
    }
    public class Errors
    {
        public Errors(string key, string errorMessage)
        {
            Key = key;
            Message = errorMessage;
        }
        public string Message { get; set; }
        public string Key { get; set; }
    }
}

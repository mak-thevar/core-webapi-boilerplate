using Newtonsoft.Json;

namespace CoreWebApiBoilerPlate.Application.DTO
{
    public class CommentResponseModel
    {
        public string CommentText { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
        [JsonProperty("CreatedBy")]
        public string CreatedByName { get; set; } = null!;
    }
}

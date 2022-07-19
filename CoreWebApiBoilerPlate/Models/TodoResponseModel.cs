using CoreWebApiBoilerPlate.DataLayer.Entities;
using Newtonsoft.Json;

namespace CoreWebApiBoilerPlate.Models
{
    public class TodoResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TodoStatus { get; set; } = null!;
        public int TodoStatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        [JsonProperty("CreatedBy")]
        public string CreatedByName { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        [JsonProperty("ModifiedBy")]
        public string? ModifiedByName { get; set; }

        public virtual IList<CommentResponseModel> Comments { get; set; }

        public TodoResponseModel()
        {
            Comments = new List<CommentResponseModel>();
        }
    }
}

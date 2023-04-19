using CoreWebApiBoilerPlate.DataLayer.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CoreWebApiBoilerPlate.DataLayer.Entities
{
    public class TodoStatus : EntityBase
    {
        [StringLength(60)]
        public string Description { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
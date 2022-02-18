using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Models
{
    public class TokenResponseModel
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

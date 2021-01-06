using Newtonsoft.Json;
using System;

namespace Invillia.ExternalService.Models
{
    public class UserLoginModelLib
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires")]
        public DateTime Expires { get; set; }

        [JsonProperty("user")]
        public UserModelLib User { get; set; }
    }
}

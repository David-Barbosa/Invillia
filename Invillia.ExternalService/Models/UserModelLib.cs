using Newtonsoft.Json;
using System;

namespace Invillia.ExternalService.Models
{
    public class UserModelLib
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}

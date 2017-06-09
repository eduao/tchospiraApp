using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TchospiraApp.Models
{
    public class News
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }

    }
}

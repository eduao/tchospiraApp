using Newtonsoft.Json;
using System.Collections.Generic;

namespace TchospiraApp.Models
{
    class Survey
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        
        public List<Option> Opcoes{ get; set; }

        [JsonProperty("banner")]
        public string Banner { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

    }
}

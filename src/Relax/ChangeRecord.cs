using Newtonsoft.Json;

namespace Relax
{
    public class ChangeRecord
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "seq")]
        public string Sequence { get; set; }
        [JsonIgnore]
        public string Document { get; set; }
    }

}

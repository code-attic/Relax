using Newtonsoft.Json;

namespace Symbiote.Relax.Impl
{
    public class SaveResponse
    {
        [JsonProperty(PropertyName = "ok")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public string Revision { get; set; }
    }
}
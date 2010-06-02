using Newtonsoft.Json;

namespace Relax.Impl.Model
{
    public class DesignView
    {
        [JsonProperty(PropertyName = "map")]
        public string Map { get; set; }

        [JsonProperty(PropertyName = "reduce")]
        public string Reduce { get; set; }
    }
}
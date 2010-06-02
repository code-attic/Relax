using Newtonsoft.Json;

namespace Relax.Impl.Json
{
    public class ViewRow<TModel>
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "doc")]
        public TModel Model { get; set; }
    }
}
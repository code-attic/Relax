using Newtonsoft.Json;

namespace Relax.Impl.Json
{
    public class BulkResponse
    {
        [JsonProperty(PropertyName = "ok")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "new_revs")]
        public NewRevision[] Revisions { get; set; }
    }
}
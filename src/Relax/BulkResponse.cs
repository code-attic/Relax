using Newtonsoft.Json;

namespace Symbiote.Relax.Impl
{
    public class BulkResponse
    {
        [JsonProperty(PropertyName = "ok")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "new_revs")]
        public NewRevision[] Revisions { get; set; }
    }
}
using System;
using Newtonsoft.Json;

namespace Relax.Impl
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class JsonDocument : CouchDocument
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Body { get; set; }
    }
}
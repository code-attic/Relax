using System;
using Newtonsoft.Json;
using Symbiote.Core.Extensions;

namespace Relax
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class DefaultCouchDocument : BaseDocument, ICouchDocument<string, string>
    {
        [JsonProperty(PropertyName = "_id")]
        public virtual string DocumentId { get; set; }

        [JsonProperty(PropertyName = "_rev")]
        public virtual string DocumentRevision { get; set; }

        public virtual string GetIdAsJson()
        {
            return DocumentId.ToJson(false);
        }

        public virtual string GetRevAsJson()
        {
            return DocumentRevision.ToJson(false);
        }

        public virtual void UpdateKeyFromJson(string jsonKey)
        {
            DocumentId = jsonKey.FromJson<string>();
        }

        public virtual void UpdateRevFromJson(string jsonRev)
        {
            DocumentRevision = jsonRev.FromJson<string>();
        }

    }
}
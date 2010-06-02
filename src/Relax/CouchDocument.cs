using System;
using Newtonsoft.Json;
using Relax.Impl;
using Symbiote.Core.Extensions;

namespace Relax
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class CouchDocument : BaseDocument, ICouchDocument<string, string>
    {
        public virtual string _docId { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public virtual string DocumentId
        {
            get 
            { 
                _docId = _docId ?? Guid.NewGuid().ToString();
                return _docId;
            }
            set
            {
                _docId = value;
            }
        }

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
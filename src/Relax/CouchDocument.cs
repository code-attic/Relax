using System;
using Newtonsoft.Json;
using Relax.Impl;
using Relax.Impl.Model;
using Symbiote.Core.Extensions;

namespace Relax
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class CouchDocument : BaseDocument, ICouchDocument<string>
    {
        [JsonIgnore]
        protected virtual string _docId { get; set; }

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

        public virtual string GetDocumentIdAsJson()
        {
            return DocumentId.ToString();
        }

        public virtual object GetDocumentId()
        {
            return DocumentId;
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
            DocumentRevision = jsonRev;
        }

    }
}
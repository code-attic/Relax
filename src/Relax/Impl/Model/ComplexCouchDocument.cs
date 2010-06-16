using System;
using Newtonsoft.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class ComplexCouchDocument<TModel, TKey> : BaseDocument, ICouchDocument<TKey>
        where TModel : ComplexCouchDocument<TModel, TKey>
    {
        protected TKey _documentId;

        protected Func<TModel, TKey> DocumentIdGetter = x => x._documentId;
        protected Action<TModel, TKey> DocumentIdSetter = (x,k) => x._documentId = k;

        protected virtual TModel KeyGetter(Func<TModel, TKey> getter)
        {
            DocumentIdGetter = getter;
            return this as TModel;
        }           

        protected virtual TModel KeySetter(Action<TModel, TKey> setter)
        {
            DocumentIdSetter = setter;
            return this as TModel;
        }

        [JsonProperty(PropertyName = "_id")]
        public virtual TKey DocumentId
        {
            get
            {
                return DocumentIdGetter(this as TModel);
            }
            set
            {
                DocumentIdSetter(this as TModel, value);
            }
        }

        [JsonProperty(PropertyName = "_rev")]
        public virtual string DocumentRevision { get; set; }

        public virtual object GetDocumentId()
        {
            return DocumentId;
        }

        public virtual string GetDocumentIdAsJson()
        {
            var typeCode = typeof (TKey);
            if(typeCode.IsValueType && typeCode.Namespace.StartsWith("System"))
            {
                return DocumentId.ToString();
            }
            else
            {
                return DocumentId.ToJson(false);
            }
        }
        
        public virtual void UpdateKeyFromJson(string jsonKey)
        {
            var documentId = jsonKey.FromJson<TKey>();
            DocumentId = object.Equals(documentId, default(TKey)) ? "\"{0}\"".AsFormat(jsonKey).FromJson<TKey>() : documentId;
        }

        public virtual void UpdateRevFromJson(string jsonRev)
        {
            DocumentRevision = jsonRev;
        }
    }
}
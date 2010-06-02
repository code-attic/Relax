using System;
using Newtonsoft.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class ComplexCouchDocument<TModel, TKey, TRev> : BaseDocument, ICouchDocument<TKey, TRev>
        where TModel : ComplexCouchDocument<TModel, TKey, TRev>
    {
        protected TKey _documentId;
        protected TRev _documentRev;

        protected Func<TModel, TKey> GetDocumentId = x => x._documentId;
        protected Func<TModel, TRev> GetDocumentRevision = x => x._documentRev;
        protected Action<TModel, TKey> SetDocumentId = (x,k) => x._documentId = k;
        protected Action<TModel, TRev> SetDocumentRevision= (x,r) => x._documentRev = r;

        protected virtual TModel KeyGetter(Func<TModel, TKey> getter)
        {
            GetDocumentId = getter;
            return this as TModel;
        }           

        protected virtual TModel KeySetter(Action<TModel, TKey> setter)
        {
            SetDocumentId = setter;
            return this as TModel;
        }

        protected virtual TModel RevisionGetter(Func<TModel, TRev> getter)
        {
            GetDocumentRevision = getter;
            return this as TModel;
        }

        protected virtual TModel RevisionSetter(Action<TModel, TRev> setter)
        {
            SetDocumentRevision = setter;
            return this as TModel;
        }

        [JsonProperty(PropertyName = "_id")]
        public virtual TKey DocumentId
        {
            get
            {
                return GetDocumentId(this as TModel);
            }
            set
            {
                SetDocumentId(this as TModel, value);
            }
        }

        [JsonProperty(PropertyName = "_rev")]
        public virtual TRev DocumentRevision
        {
            get
            {
                return GetDocumentRevision(this as TModel);
            }
            set
            {
                SetDocumentRevision(this as TModel, value);
            }
        }

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
            var documentId = jsonKey.FromJson<TKey>();
            DocumentId = object.Equals(documentId, default(TKey)) ? "\"{0}\"".AsFormat(jsonKey).FromJson<TKey>() : documentId;
        }

        public virtual void UpdateRevFromJson(string jsonRev)
        {
            var documentRevision = jsonRev.FromJson<TRev>();
            DocumentRevision = object.Equals(documentRevision, default(TRev)) ? "\"{0}\"".AsFormat(jsonRev).FromJson<TRev>() : documentRevision;
        }
    }
}
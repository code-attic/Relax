using Relax.Impl.Configuration;
using Relax.Impl.Json;
using Relax.Impl.Model;
using Symbiote.Core.Extensions;
using Symbiote.Core.Reflection;

namespace Relax.Impl
{
    public class CouchUtility
    {
        protected ICouchConfiguration configuration { get; set; }

        public virtual string GetDocumentId(object instance)
        {
            var doc = instance as IHandleJsonDocumentId;
            if (doc != null)
            {
                return doc.GetIdAsJson();
            }
            else
            {
                return JsonExtensions.ToJson<object>(Reflector.ReadMember(instance, configuration.Conventions.IdPropertyName), false);
            }
        }

        public virtual string GetDocumentRevision(object instance)
        {
            var doc = instance as IHandleJsonDocumentRevision;
            if (doc != null)
            {
                return doc.GetRevAsJson();
            }
            else
            {
                return JsonExtensions.ToJson<object>(Reflector.ReadMember(instance, configuration.Conventions.RevisionPropertyName), false);
            }
        }

        public virtual void SetDocumentId(string json, object instance)
        {
            var doc = instance as IHandleJsonDocumentId;
            var response = json.FromJson<SaveResponse>();
            if (doc != null)
            {
                doc.UpdateKeyFromJson(response.Id);
            }
            else
            {
                var idType = Reflector.GetMemberType(instance.GetType(), configuration.Conventions.IdPropertyName);
                Reflector.WriteMember(instance, configuration.Conventions.IdPropertyName, response.Id.FromJson(idType));
            }
        }

        public virtual void SetDocumentRevision(string json, object instance)
        {
            var doc = instance as IHandleJsonDocumentRevision;
            var response = json.FromJson<SaveResponse>();
            if (doc != null)
            {
                doc.UpdateRevFromJson(response.Id);
            }
            else
            {
                var idType = Reflector.GetMemberType(instance.GetType(), configuration.Conventions.RevisionPropertyName);
                Reflector.WriteMember(instance, configuration.Conventions.RevisionPropertyName, response.Id.FromJson(idType));
            }
        }

        public CouchUtility(ICouchConfiguration couchConfiguration)
        {
            configuration = couchConfiguration;
        }
    }
}
using Newtonsoft.Json.Serialization;

namespace Relax.Impl.Serialization
{
    public class ConventionDeserializationContractResolver : DefaultContractResolver
    {
        protected DocumentConventions conventions { get; set; }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName == conventions.IdPropertyName)
                return "_id";
            else if (propertyName == conventions.RevisionPropertyName)
                return "_rev";
            else
                return base.ResolvePropertyName(propertyName);
        }

        public ConventionDeserializationContractResolver(DocumentConventions conventions)
        {
            this.conventions = conventions;
        }
    }
}
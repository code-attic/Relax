using System.Collections.Generic;
using System.Linq;
using Relax.Config;

namespace Relax.Impl.Serialization
{
    public class ConventionSerializationContractResolver : ChildDocumentContractResolver
    {
        protected DocumentConventions conventions { get; set; }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName == "Id")
                return "_id";
            else if (propertyName == "Rev")
                return "_rev";
            else
                return base.ResolvePropertyName(propertyName);
        }

        public ConventionSerializationContractResolver(ICouchConfiguration configuration, DocumentConventions conventions) : base(configuration)
        {
            this.conventions = conventions;
        }
    }
}
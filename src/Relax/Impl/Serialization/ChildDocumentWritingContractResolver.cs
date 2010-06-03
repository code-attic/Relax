using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Serialization
{
    public class ChildDocumentResolverStrategy : IContractResolverStrategy
    {
        public bool ResolverAppliesForSerialization(Type type)
        {
            return true;
        }

        public bool ResolverAppliesForDeserialization(Type type)
        {
            return false;
        }

        public IContractResolver Resolver
        {
            get { return new ChildDocumentContractResolver(); }
        }
    }

    public class ChildDocumentContractResolver : DefaultContractResolver
    {
        private const string ICOUCHDOC_TYPE = "ICouchDocument`2";
        private const string IENUMERABLE_TYPE = "IEnumerable`1";

        protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
        {
            var basePropertyList = base.CreateProperties(contract);
            return basePropertyList
                .Where(ShouldIncludeProperty)
                .ToList();
        }

        protected bool ShouldIncludeProperty(JsonProperty property)
        {
            var propertyType = property.PropertyType;
            var interfaces = propertyType.GetInterfaces();
            if(interfaces.Any(x => x.Name == IENUMERABLE_TYPE) && propertyType.IsGenericType)
            {
                var paramType = propertyType.GetGenericArguments()[0];
                if (paramType == null || paramType.GetInterface(ICOUCHDOC_TYPE) == null)
                    return true;
            }
            else if (propertyType.GetInterface(ICOUCHDOC_TYPE) == null)
            {
                return true;
            }
            return false;
        }
    }
}

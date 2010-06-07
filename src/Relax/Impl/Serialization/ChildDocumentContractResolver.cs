using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Relax.Impl.Configuration;

namespace Relax.Impl.Serialization
{
    public class ChildDocumentContractResolver : DefaultContractResolver
    {
        private const string ICOUCHDOC_TYPE = "ICouchDocument`2";
        private const string IENUMERABLE_TYPE = "IEnumerable`1";

        private ICouchConfiguration configuration { get; set; }

        protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
        {
            var basePropertyList = base.CreateProperties(contract);
            return 
                !configuration.BreakDownDocumentGraphs ?
                basePropertyList :
                basePropertyList
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

        public ChildDocumentContractResolver(ICouchConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ChildDocumentContractResolver(bool shareCache, ICouchConfiguration configuration)
            : base(shareCache)
        {
            this.configuration = configuration;
        }
    }
}
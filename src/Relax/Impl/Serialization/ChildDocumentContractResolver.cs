using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Relax.Impl.Serialization
{
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
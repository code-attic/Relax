using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace Relax.Impl.Serialization
{
    internal class ChildDocumentWritingContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
        {
            

            return base.CreateProperties(contract);
        }
    }
}

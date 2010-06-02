using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Relax.Impl
{
    internal class ChildDocumentWritingContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
        {
            

            return base.CreateProperties(contract);
        }
    }
}

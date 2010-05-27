using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Relax.Impl
{
    public class BulkPersist
    {
        [JsonProperty(PropertyName = "all_or_nothing")]
        public bool AllOrNothing { get; set; }

        [JsonProperty(PropertyName = "non_atomic")]
        public bool NonAtomic { get; set; }

        [JsonProperty(PropertyName = "docs")]
        public object[] Documents { get; set; }

        public BulkPersist(IEnumerable<object> docs)
        {
            Documents = docs.ToArray();
        }

        public BulkPersist(bool allOrNothing, bool nonAtomic, IEnumerable<object> docs)
        {
            AllOrNothing = allOrNothing;
            NonAtomic = nonAtomic;
            Documents = docs.ToArray();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Symbiote.Relax.Impl
{
    public class BulkPersist<TModel>
        where TModel : class
    {
        [JsonProperty(PropertyName = "all_or_nothing")]
        public bool AllOrNothing { get; set; }

        [JsonProperty(PropertyName = "non_atomic")]
        public bool NonAtomic { get; set; }

        [JsonProperty(PropertyName = "docs")]
        public TModel[] Documents { get; set; }

        public BulkPersist(IEnumerable<TModel> docs)
        {
            Documents = docs.ToArray();
        }

        public BulkPersist(bool allOrNothing, bool nonAtomic, IEnumerable<TModel> docs)
        {
            AllOrNothing = allOrNothing;
            NonAtomic = nonAtomic;
            Documents = docs.ToArray();
        }
    }
}
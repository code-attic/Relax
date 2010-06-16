using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Json
{
    public class BulkPersist
    {
        protected static readonly string pattern = @"(?<=values[""][:])(.+)(?=[\}])";

        [JsonProperty(PropertyName = "all_or_nothing")]
        public bool AllOrNothing { get; set; }

        [JsonProperty(PropertyName = "non_atomic")]
        public bool NonAtomic { get; set; }

        [JsonProperty(PropertyName = "docs")]
        public object[] Documents { get; set; }

        public BulkPersist()
        {
        }

        public override string ToString()
        {
            var regex = new Regex(pattern);
            var docList = Documents.ToJson();
            var array = regex.Match(docList).Value;
            return "{{\"all_or_nothing\":{0}, \"non_atomic\":{1}, \"docs\":{2} }}"
                .AsFormat(AllOrNothing.ToString().ToLower(), NonAtomic.ToString().ToLower(), array);
        }

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
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Relax.Impl
{
    public class ViewResult<TModel>
    {
        [JsonProperty(PropertyName = "total_rows")]
        public int TotalRows { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public ViewRow<TModel>[] Rows { get; set; }

        public IEnumerable<TModel> GetList()
        {
            return Rows.Select(x => x.Model);
        }
    }
}
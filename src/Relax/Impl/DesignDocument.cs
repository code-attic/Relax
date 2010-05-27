using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Relax.Impl
{
    public class DesignDocument : CouchDocument<DesignDocument, string, string>
    {
        [JsonProperty(PropertyName = "views")]
        public Dictionary<string, DesignView> Views { get; set; }

        public DesignDocument()
        {
            Views = new Dictionary<string, DesignView>();
        }
    }

    public class DesignView
    {
        [JsonProperty(PropertyName = "map")]
        public string Map { get; set; }

        [JsonProperty(PropertyName = "reduce")]
        public string Reduce { get; set; }
    }
}

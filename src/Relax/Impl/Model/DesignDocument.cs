using System.Collections.Generic;
using Newtonsoft.Json;

namespace Relax.Impl.Model
{
    public class DesignDocument : ComplexCouchDocument<DesignDocument, string>
    {
        [JsonProperty(PropertyName = "views")]
        public Dictionary<string, DesignView> Views { get; set; }

        public DesignDocument()
        {
            Views = new Dictionary<string, DesignView>();
        }
    }
}

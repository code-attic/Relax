using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Relax.Impl
{
    public class DesignDocument : ComplexCouchDocument<DesignDocument, string, string>
    {
        [JsonProperty(PropertyName = "views")]
        public Dictionary<string, DesignView> Views { get; set; }

        public DesignDocument()
        {
            Views = new Dictionary<string, DesignView>();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Relax.Impl
{
    public class DocumentConventions
    {
        public string IdPropertyName { get; set; }
        public string RevisionPropertyName { get; set; }

        public DocumentConventions()
        {
            IdPropertyName = "Id";
            RevisionPropertyName = "Rev";
        }
    }
}

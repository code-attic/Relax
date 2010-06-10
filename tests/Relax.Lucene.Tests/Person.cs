using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Store;
using Newtonsoft.Json.Linq;
using Relax.Impl.Commands;
using Relax.Impl.Http;
using Relax.Impl.Model;
using StructureMap.Pipeline;

namespace Relax.Lucene.Tests
{
    public class Person : CouchDocument
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual List<Car> Cars { get; set; }
    }
}

using System.Collections.Generic;

namespace Relax.Tests.Commands
{
    public class TestDoc : CouchDocument
    {
        public virtual string Message { get; set; }
    }

    public class ParentDoc : CouchDocument
    {
        public virtual string Message { get; set; }
        public virtual List<TestDoc> Children { get; set; }
    }
}
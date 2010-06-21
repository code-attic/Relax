using System;

namespace Relax.Tests
{
    public class SimpleDocument : CouchDocument
    {
        public virtual string Message { get; set; }
        public virtual DateTime CreatedOn { get; set; }

        public SimpleDocument()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
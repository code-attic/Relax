using System;
using Relax.Impl;
using Relax.Impl.Model;

namespace Relax.Tests
{
    public class TestDocument : ComplexCouchDocument<TestDocument, Guid>
    {
        public virtual string Message { get; set; }
        public virtual DateTime CreatedOn { get; set; }

        public TestDocument()
        {
            _documentId = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }
    }
}
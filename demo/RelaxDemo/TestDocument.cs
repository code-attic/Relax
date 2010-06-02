using System;
using Relax;
using Relax.Impl;

namespace RelaxDemo
{
    [Serializable]
    public class TestDocument : ComplexCouchDocument<TestDocument, Guid, string>
    {
        public virtual string Message { get; set; }
        public virtual DateTime Time { get; set; }

        public TestDocument()
        {
            _documentId = Guid.NewGuid();
        }

        public TestDocument(string message)
        {
            _documentId = Guid.NewGuid();
            Message = message;
            Time = DateTime.Now;
        }
    }
}
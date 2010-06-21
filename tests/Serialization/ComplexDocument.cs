using System;
using Relax.Impl.Model;

namespace Relax.Tests.Serialization
{
    [Serializable]
    public class ComplexDocument : ComplexCouchDocument<ComplexDocument, Guid>
    {
        public virtual string Message { get; set; }
        public virtual DateTime Time { get; set; }

        public ComplexDocument()
        {
            _documentId = Guid.NewGuid();
        }

        public ComplexDocument(string message)
        {
            _documentId = Guid.NewGuid();
            Message = message;
            Time = DateTime.Now;
        }
    }
}
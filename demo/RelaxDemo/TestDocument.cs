using System;
using System.Collections.Generic;
using Relax;
using Relax.Impl;
using Relax.Impl.Model;

namespace RelaxDemo
{
    [Serializable]
    public class TestDocument 
        : ComplexCouchDocument<TestDocument, Guid, string>
    {
        public virtual string Message { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual int RandomNumber { get; set; }

        public TestDocument()
        {
            _documentId = Guid.NewGuid();
            var rnd = new Random();
            RandomNumber = rnd.Next(100);
        }

        public TestDocument(string message)
        {
            _documentId = Guid.NewGuid();
            Message = message;
            Time = DateTime.Now;
            var rnd = new Random();
            RandomNumber = rnd.Next(100);
        }
    }
}
using System.Collections.Generic;

namespace Relax.Tests.Serialization
{
    public class ClassA : CouchDocument
    {
        public ClassB B { get; set; }
        public List<ClassC> Cs { get; set; }
        public List<ClassD> Ds { get; set; }
        public ClassD E { get; set; }
        public List<ClassF> Fs { get; set; }
    }
}
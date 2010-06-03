using System.Collections.Generic;

namespace Relax.Tests.Serialization.Filtering
{
    public class ClassA : CouchDocument
    {
        public ClassB B { get; set; }
        public List<ClassC> Cs { get; set; }
        public List<ClassD> Ds { get; set; }
        public ClassE E { get; set; }
        public List<ClassF> Fs { get; set; }
    }
}
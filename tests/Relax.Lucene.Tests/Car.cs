namespace Relax.Lucene.Tests
{
    public class Car : CouchDocument
    {
        public virtual string Make { get; set; }
        public virtual string Model { get; set; }
        public virtual int Year { get; set; }
    }
}
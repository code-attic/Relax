namespace Relax.Tests.Serialization
{
    public class ClassD : CouchDocument
    {
        public string Message { get; set; }

        public ClassD(string message)
        {
            Message = message;
        }
    }
}
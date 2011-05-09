using Machine.Specifications;
using Relax.Impl.Http;

namespace Relax.Tests.URI
{
    public abstract class with_basic_uri
    {
        protected static CouchUri uri;
        private Establish context = () => uri = CouchUri.Build("http", "localhost", 5984, "relax");
    }
}
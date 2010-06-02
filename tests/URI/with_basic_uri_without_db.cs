using Machine.Specifications;
using Relax.Impl;
using Relax.Impl.Http;

namespace Relax.Tests.URI
{
    public abstract class with_basic_uri_without_db
    {
        protected static CouchUri uri;
        private Establish context = () => uri = CouchUri.Build("http", "localhost", 5984);
    }
}
using Machine.Specifications;
using Moq;
using Relax.Impl.Http;

namespace Relax.Tests.URI.ViewQuery
{
    public abstract class with_couch_uri
    {
        protected static Mock<CouchUri> uriMock;
        protected static CouchUri uri { get { return uriMock.Object; } }
        protected static Relax.Impl.Commands.ViewQuery query { get { return new Relax.Impl.Commands.ViewQuery(uri); } }

        private Establish context = () =>
                                        {
                                            uriMock = new Mock<CouchUri>("http", "localhost", 5984);
                                        };
    }
}

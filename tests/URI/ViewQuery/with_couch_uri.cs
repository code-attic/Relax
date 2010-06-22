using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using It = Moq.It;

namespace Relax.Tests.URI.ViewQuery
{
    public abstract class with_couch_uri
    {
        protected static Mock<CouchUri> uriMock;
        protected static CouchUri uri { get { return uriMock.Object; } }
        protected static Impl.Commands.ViewQuery query { get { return new Impl.Commands.ViewQuery(uri); } }

        private Establish context = () =>
                                        {
                                            uriMock = new Mock<CouchUri>("http", "localhost", 5984);
                                        };
    }
}

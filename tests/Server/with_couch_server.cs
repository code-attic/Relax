using Machine.Specifications;
using Moq;
using Relax.Impl;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Symbiote.Core;

namespace Relax.Tests.Server
{
    public abstract class with_couch_server : with_configuration
    {
        protected static ICouchServer server;
        protected static CouchUri uri;
        protected static Mock<IHttpAction> commandMock;
        protected static CouchUri couchUri
        {
            get
            {
                //var config = Assimilate.GetInstanceOf<ICouchConfiguration>();
                //config.Cache = false;
                Assimilate.Dependencies(x => x.For<IDocumentRepository>().Use<DocumentRepository>());
                return Moq.It.Is<CouchUri>(u => u.ToString().Equals(uri.ToString()));
            }
        }

        private Establish context = () =>
        {
            commandMock = new Mock<IHttpAction>();
            server = Assimilate.GetInstanceOf<CouchDbServer>();
        };
    }
}

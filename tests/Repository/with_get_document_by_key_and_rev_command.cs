using System;
using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Symbiote.Core;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Repository
{
    public abstract class with_get_document_by_key_and_rev_command : with_configuration
    {
        protected static Guid id;
        protected static Mock<IHttpAction> commandMock;
        protected static CouchUri uri;
        protected static IDocumentRepository repository;
        protected static CouchUri couchUri 
        {
            get
            {
                return Moq.It.Is<CouchUri>(u => u.ToString().Equals(uri.ToString()));
            }
        }

        private Establish context = () =>
                                        {
                                            repository = Assimilate.GetInstanceOf<DocumentRepository>();
                                            commandMock = new Mock<IHttpAction>();
                                            id = Guid.NewGuid();
                                            uri = new CouchUri("http", "localhost", 5984, "relax")
                                                .IdAndRev(id, "2");
                                            commandMock
                                                .Setup(x => x.Get( couchUri ))
                                                .Returns("{{ _id : \"{0}\", _rev : \"2\", Message : \"Hello\"}}".AsFormat(id));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
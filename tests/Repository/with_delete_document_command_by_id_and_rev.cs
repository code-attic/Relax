using System;
using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Symbiote.Core;

namespace Relax.Tests.Repository
{
    public abstract class with_delete_document_command_by_id_and_rev : with_configuration
    {
        protected static Guid id;
        protected static Mock<IHttpAction> commandMock;
        protected static CouchUri uri;
        protected static IDocumentRepository repository;
        protected static CouchUri couchUri 
        {
            get
            {
                return Moq.It.Is<CouchUri>( u => u.ToString().Equals( uri.ToString() ) );
            }
        }

        private Establish context = () =>
                                        {
                                            repository = Assimilate.GetInstanceOf<DocumentRepository>();
                                            commandMock = new Mock<IHttpAction>();
                                            //id = Guid.NewGuid();
                                            id = Guid.Parse( "00000000-0000-0000-0000-000000000001" );
                                            uri = new CouchUri("http", "localhost", 5984, "relax").IdAndRev(id, "1");
                                            commandMock.Setup( x => x.Delete( couchUri ) );
                                            WireUpCommandMock( commandMock.Object );
                                        };
    }
}
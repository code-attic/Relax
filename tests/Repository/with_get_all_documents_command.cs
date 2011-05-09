using System;
using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Symbiote.Core;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Repository
{
    public abstract class with_get_all_documents_command : with_configuration
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
                                            uri = new CouchUri("http", "localhost", 5984, "relax")
                                                .ListAll()
                                                .IncludeDocuments();
                                            commandMock.Setup(x => x.Get( couchUri ))
                                                .Returns("{{ offset: \"0\", total_rows: \"1\", rows : [ {{ id : \"{0}\", key : \"{0}\", doc : {{_id : \"{0}\", _rev : \"2\", Message : \"Hello\" }} }} ] }}".AsFormat(id));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
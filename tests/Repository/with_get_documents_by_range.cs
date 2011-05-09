using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Symbiote.Core;

namespace Relax.Tests.Repository
{
    public abstract class with_get_documents_by_range : with_configuration
    {
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
                                                .IncludeDocuments()
                                                .StartKey("1")
                                                .EndKey("2");

                                            commandMock.Setup(x => x.Get( couchUri ))
                                                .Returns(
                                                    "{ offset: \"0\", total_rows: \"1\", rows : [ { id : \"1\", key : \"1\", doc : {_id : \"1\", _rev : \"2\", Message : \"Hello\" } } ] }");

                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
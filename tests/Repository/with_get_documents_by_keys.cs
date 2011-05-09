using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Relax.Impl.Repository;
using Symbiote.Core;
using Symbiote.Core.Serialization;

namespace Relax.Tests.Repository
{
    public abstract class with_get_documents_by_keys : with_configuration
    {
        protected static KeyList keyList;
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

        protected static string KeyList
        {
            get { return Moq.It.Is<string>( k => k.Equals( keyList.ToJson( false ) ) ); }
        }

        private Establish context = () =>
                                        {
                                            repository = Assimilate.GetInstanceOf<DocumentRepository>();
                                            uri = new CouchUri("http", "localhost", 5984, "relax")
                                                .ListAll()
                                                .IncludeDocuments();

                                            keyList = new KeyList() {keys = new object[] {"1"}};
                                            var jsonKeyList = keyList.ToJson(false);
                                            commandMock = new Mock<IHttpAction>();
                                            commandMock.Setup(x => x.Post( couchUri, KeyList ))
                                                .Returns(
                                                    "{ offset: \"0\", total_rows: \"1\", rows : [ { id : \"1\", key : \"1\", doc : {_id : \"1\", _rev : \"2\", Message : \"Hello\" } } ] }");

                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
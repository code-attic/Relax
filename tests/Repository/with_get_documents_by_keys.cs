using Machine.Specifications;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Repository
{
    public abstract class with_get_documents_by_keys : with_document_repository
    {
        protected static KeyList keyList;
        private Establish context = () =>
                                        {
                                            uri = new CouchUri("http", "localhost", 5984, "relax")
                                                .ListAll()
                                                .IncludeDocuments();

                                            keyList = new KeyList() {keys = new object[] {"1"}};
                                            var jsonKeyList = keyList.ToJson(false);

                                            commandMock.Setup(x => x.Post(couchUri, jsonKeyList))
                                                .Returns(
                                                    "{ offset: \"0\", total_rows: \"2\", rows : [ { id : \"1\", key : \"1\", doc : {_id : \"1\", _rev : \"2\", Message : \"Hello\" } } ] }");

                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
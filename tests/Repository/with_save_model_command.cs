using System;
using Machine.Specifications;
using Relax.Impl;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;
using It = Moq.It;

namespace Relax.Tests.Repository
{
    public abstract class with_save_model_command : with_test_document
    {
        protected static Guid id;
        protected static BulkPersist bulkSave;
        protected static string bulkSaveJson;

        private Establish context = () =>
                                        {
                                            id = Guid.NewGuid();
                                            document = new TestDocument()
                                            {
                                                DocumentId = id,
                                                Message = "Hello",
                                                DocumentRevision = "2"
                                            };
                                            bulkSave = new BulkPersist(true, false, new[] { document });
                                            bulkSaveJson = bulkSave.ToString();

                                            uri = new CouchUri("http", "localhost", 5984, "relax").BulkInsert();
                                            var saveResponse =
                                                new[]
                                                    {
                                                        new SaveResponse() {Id = id.ToString(), Revision = "3", Success = true}
                                                    };

                                            commandMock.Setup(x => x.Post(couchUri, It.Is<string>(s => s.Equals(bulkSaveJson))))
                                                .Returns(saveResponse.ToJson(false));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
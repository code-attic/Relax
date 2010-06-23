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
        protected static string json;

        private Establish context = () =>
                                        {
                                            id = Guid.NewGuid();
                                            document = new TestDocument()
                                            {
                                                DocumentId = id,
                                                Message = "Hello",
                                                DocumentRevision = "2"
                                            };
                                            json = document.ToJson();

                                            uri = new CouchUri("http", "localhost", 5984, "relax").Id(id);
                                            var saveResponse = new SaveResponse() {Id = id.ToString(), Revision = "3", Success = true};

                                            commandMock.Setup(x => x.Put(couchUri, It.Is<string>(s => s.Equals(json))))
                                                .Returns(saveResponse.ToJson(false));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
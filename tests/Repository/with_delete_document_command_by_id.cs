using System;
using Machine.Specifications;
using Relax.Impl.Http;
using Relax.Tests.Commands;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Repository
{
    public abstract class with_delete_document_command_by_id : with_document_repository
    {
        protected static Guid id;
        protected static CouchUri getUri;
        protected static TestDoc document;

        private Establish context = () =>
                                        {
                                            id = Guid.NewGuid();
                                            document = new TestDoc() { DocumentId = id.ToString(), DocumentRevision = "1"};
                                            uri = new CouchUri("http", "localhost", 5984, "relax").IdAndRev(id, "1");
                                            getUri = new CouchUri("http", "localhost", 5984, "relax").Id(id);

                                            commandMock.Setup(x => x.Delete(couchUri));
                                            commandMock
                                                .Setup(x => x.Get(Moq.It.Is<CouchUri>(u => u.ToString().Equals(getUri.ToString()))))
                                                .Returns(document.ToJson());

                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
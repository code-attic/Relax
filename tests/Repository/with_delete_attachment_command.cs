using Machine.Specifications;
using Relax.Impl.Http;
using Relax.Tests.Commands;

namespace Relax.Tests.Repository
{
    public abstract class with_delete_attachment_command : with_document_repository
    {
        protected static string attachmentName;
        protected static TestDoc document;
        
        private Establish context = () =>
                                        {
                                            document = new TestDoc() {DocumentId = "1", DocumentRevision = "1"};
                                            attachmentName = "myattachment";
                                            uri = new CouchUri("http", "localhost", 5984, "relax")
                                                .Id(document.DocumentId)
                                                .Attachment(attachmentName)
                                                .Revision(document.DocumentRevision);
                                            commandMock
                                                .Setup(x => x.Delete(couchUri));
                                            WireUpCommandMock(commandMock.Object);
                                        };
    }
}
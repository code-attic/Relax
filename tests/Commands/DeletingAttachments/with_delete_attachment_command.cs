using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Http;

namespace Relax.Tests.Commands.DeletingAttachments
{
    public abstract class with_delete_attachment_command : with_command_factory
    {
        protected static DeleteAttachmentCommand command;
        protected static string jsonResult;
        protected static string url;
        protected static TestDoc document;

        private Establish context = () =>
                                        {
                                            document = new TestDoc()
                                                           {
                                                               DocumentId = "test",
                                                               DocumentRevision = "01"
                                                           };
                                            document.AddAttachment("myattachment", "text", 128);

                                            url = @"http://localhost:5984/relax/test/myattachment?rev=01";
                                            jsonResult = @"{ ok: true, rev: ""02""}";
                                            mockAction
                                                .Setup(x => x.Delete(Moq.It.Is<CouchUri>(u => u.ToString() == url)))
                                                .Returns(jsonResult);

                                            command = factory.CreateDeleteAttachmentCommand();
                                        };
    }
}

using System.Text;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Http;

namespace Relax.Tests.Commands.SavingAttachments
{
    public abstract class with_save_attachment_command : with_command_factory
    {
        protected static string url;
        protected static string attachment;
        protected static byte[] bytes;
        protected static TestDoc document;
        protected static SaveAttachmentCommand command;

        private Establish context = () =>
                                        {
                                            attachment = "123, look at me";
                                            document = new TestDoc() {DocumentId = "1", DocumentRevision = "1"};
                                            bytes = Encoding.UTF8.GetBytes(attachment);

                                            url = @"http://localhost:5984/relax/1/test?rev=1";

                                            mockAction
                                                .Setup(x => x.SaveAttachment(Moq.It.Is<CouchUri>(u => u.ToString() == url), "text", bytes))
                                                .Returns(@"{rev:2}");
                                            command = factory.CreateSaveAttachmentCommand();
                                        };
    }
}

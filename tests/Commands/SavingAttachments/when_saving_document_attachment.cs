using System.Linq;
using Machine.Specifications;

namespace Relax.Tests.Commands.SavingAttachments
{
    public class when_saving_document_attachment : with_save_attachment_command
    {
        private Because of = () =>
                                 {
                                     command.SaveAttachment(document, "test", "text", bytes);
                                 };

        private It should_update_revision = () => 
                                            document.DocumentRevision.ShouldEqual("2");

        private It should_add_attachment_to_document = () => document.Attachments.Count().ShouldEqual(1);
        private It should_have_attachment = () => document.Attachments.First().ShouldEqual("test");
        private It should_call_action = () => mockAction.VerifyAll();
    }
}
using Machine.Specifications;

namespace Relax.Tests.Commands.SaveAttachmentCommand
{
    public class when_saving_document_attachment : with_save_attachment_command
    {
        private Because of = () =>
                                 {
                                     command.SaveAttachment(document, "test", "text", bytes);
                                 };

        private It should_update_revision = () => 
                                            document.DocumentRevision.ShouldEqual("2");

        private It should_call_action = () => mockAction.VerifyAll();
    }
}
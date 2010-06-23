using System.Linq;
using Machine.Specifications;

namespace Relax.Tests.Commands.DeletingAttachments
{
    public class when_deleting_attachment : with_delete_attachment_command
    {
        private Because of = () =>
                                 {
                                     command.DeleteAttachment(document, "myattachment");
                                 };

        private It should_update_revision = () => document.DocumentRevision.ShouldEqual("02");
        private It should_remove_attachment = () => ShouldExtensionMethods.ShouldEqual(document.Attachments.Count(), 0);
    }
}
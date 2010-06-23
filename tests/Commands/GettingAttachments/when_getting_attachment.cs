using System;
using System.Text;
using Machine.Specifications;

namespace Relax.Tests.Commands.GettingAttachments
{
    public class when_getting_attachment : with_get_attachment_command
    {
        protected static Tuple<string, byte[]> expectedAttachment;
        protected static string content;

        private Because of = () =>
                                 {
                                     expectedAttachment = command.GetAttachment<TestDoc>("test", "myattachment");
                                     content = Encoding.UTF8.GetString(expectedAttachment.Item2);
                                 };

        private It should_have_attachment_name = () => expectedAttachment.Item1.ShouldEqual("myattachment");
        private It should_have_correct_content = () => content.ShouldEqual("this is a lame attachment :(");
    }
}
using System;
using System.Text;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Http;

namespace Relax.Tests.Commands.GettingAttachments
{
    public abstract class with_get_attachment_command : with_command_factory
    {
        protected static string url;
        protected static Tuple<string, byte[]> attachment;
        protected static GetAttachmentCommand command;

        private Establish context = () =>
                                        {
                                            url = @"http://localhost:5984/relax/test/myattachment";

                                            var content = "this is a lame attachment :(";
                                            var bytes = Encoding.UTF8.GetBytes(content);
                                            attachment = Tuple.Create("myattachment", bytes);

                                            mockAction
                                                .Setup(
                                                    x => x.GetAttachment(Moq.It.Is<CouchUri>(u => u.ToString() == url)))
                                                .Returns(attachment);
                                            command = factory.CreateGetAttachmentCommand();
                                        };
    }
}

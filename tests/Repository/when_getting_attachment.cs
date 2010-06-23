using System;
using System.Text;
using Machine.Specifications;
using Relax.Tests.Commands;

namespace Relax.Tests.Repository
{
    public class when_getting_attachment : with_get_attachment
    {
        protected static Tuple<string, byte[]> attachmentResult;
        protected static string contentResult;
        protected static Exception exception;

        private Because of = () =>
                                 {
                                     exception =
                                         Catch.Exception(
                                             () => attachmentResult = repository.GetAttachment<TestDoc>("1", "myattachment"));
                                     contentResult = Encoding.UTF8.GetString(attachmentResult.Item2);
                                 };

        private It should_get_correct_attachment = () => attachmentResult.Item1.ShouldEqual("myattachment");
        private It should_have_correct_content = () => contentResult.ShouldEqual(content);
        private It should_get_documents_without_exception = () => exception.ShouldBeNull();
        private It should_call_get_correctly = () => commandMock.VerifyAll();
    }
}
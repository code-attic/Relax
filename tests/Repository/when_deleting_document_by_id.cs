using System;
using Machine.Specifications;
using Relax.Tests.Commands;

namespace Relax.Tests.Repository
{
    public class when_deleting_document_by_id : with_delete_document_command_by_id
    {
        private static Exception exception = null;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => repository.DeleteDocument<TestDoc>(id)
                                         );
                                 };

        private It should_delete_document_without_exception = () => exception.ShouldBeNull();
        private It should_call_delete_correctly = () => commandMock.VerifyAll();
    }
}
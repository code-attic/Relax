using System;
using Machine.Specifications;

namespace Relax.Tests.Repository
{
    public class when_deleting_document_by_id_and_rev : with_delete_document_command_by_id_and_rev
    {
        private static Exception exception = null;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => repository.DeleteDocument<TestDocument>(id, "1")
                                         );
                                 };

        private It should_delete_document_without_exception = () => exception.ShouldBeNull();
        private It should_call_delete_correctly = () => commandMock.VerifyAll();
    }
}
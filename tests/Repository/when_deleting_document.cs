using System;
using Machine.Specifications;

namespace Relax.Tests.Repository
{
    public class when_deleting_document : with_delete_document_command
    {
        private static Exception exception = null;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => repository.DeleteDocument<TestDocument>(id, "1")
                                         );
                                 };

        private It should_delete_document_without_exception = () => exception.ShouldBeNull();
        private It should_call_delete_correctly = () => commandMock.Verify(x => x.Delete(couchUri));
    }
}
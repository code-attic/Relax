using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_deleteing_database : with_create_delete_database_command
    {
        private static Exception exception = null;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => server.DeleteDatabase<TestDocument>()
                                         );
                                 };

        private It should_delete_database_without_exception = () => exception.ShouldBeNull();
        private It should_call_delete_correctly = () => commandMock.Verify(x => x.Delete(couchUri));
    }
}
using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_creating_database : with_create_database_command
    {
        private static Exception exception = null;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => server.CreateDatabase<TestDocument>()
                                     );
                                 };

        private It should_create_database_without_exception = () => exception.ShouldBeNull();
        private It should_call_put_correctly = () => commandMock.Verify(x => x.Put(couchUri));
    }
}

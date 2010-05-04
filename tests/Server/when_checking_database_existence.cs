using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_checking_database_existence : with_check_database_exists_command
    {
        private static Exception exception = null;
        private static bool exists = false;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => exists = server.DatabaseExists<TestDocument>()
                                         );
                                 };

        private It should_check_without_exception = () => exception.ShouldBeNull();
        private It should_find_database = () => exists.ShouldBeTrue();
        private It should_call_get_correctly = () => commandMock.Verify(x => x.Get(couchUri));
    }
}
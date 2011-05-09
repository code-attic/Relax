using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_getting_list_of_databases : with_list_databases_command
    {
        private static Exception exception = null;
        private static IList<string> databases;
        private Because of = () =>
                                 {
                                     exception = Catch.Exception(
                                         () => databases = server.DatabaseList
                                         );
                                 };

        private It should_get_list_without_exception = () => exception.ShouldBeNull();
        private It should_get_database_list = () => databases
                                                        .ShouldContain("one", "two", "three");
        private It should_call_get_correctly = () => commandMock.VerifyAll();
    }
}
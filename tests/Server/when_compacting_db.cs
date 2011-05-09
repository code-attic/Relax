using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_compacting_db : with_db_compaction
    {
        private static Exception exception;

        private Because of = () =>
                                 {
                                     exception = Catch.Exception(() => server.CompactDatabase<TestDocument>());
                                 };

        private It should_not_throw_exception = () => exception.ShouldBeNull();
        private It should_call_clean_view_command = () => commandMock.VerifyAll();
    }
}
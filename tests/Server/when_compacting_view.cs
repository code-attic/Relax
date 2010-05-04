using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_compacting_view : with_view_compaction
    {
        private static Exception exception;

        private Because of = () =>
                                 {
                                     exception = Catch.Exception(() => server.CompactView<TestDocument>("testView"));
                                 };

        private It should_not_throw_exception = () => exception.ShouldBeNull();
        private It should_call_clean_view_command = () => commandMock.Verify(x => x.Post(couchUri));
    }
}
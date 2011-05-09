using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_cleaning_up_views : with_view_cleanup_command
    {
        private static Exception exception;

        private Because of = () =>
                                 {
                                     exception = Catch.Exception(() => server.CleanViews<TestDocument>());
                                 };

        private It should_not_throw_exception = () => exception.ShouldBeNull();
        private It should_call_clean_view_command = () => commandMock.VerifyAll();
    }
}
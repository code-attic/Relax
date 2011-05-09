using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_copying_from_local_database : with_copy_replication
    {
        protected static Exception exception;

        private Because of = () =>
                                 {
                                     exception = Catch.Exception(() => server.CopyDatabase<TestDocument>(targetUri));
                                 };

        private It should_copy_without_exceptions = () => exception.ShouldBeNull();
        private It should_call_replicate_via_post = () => commandMock.VerifyAll();
    }
}
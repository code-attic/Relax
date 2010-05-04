using System;
using Machine.Specifications;

namespace Relax.Tests.Server
{
    public class when_starting_replication_from_local_database : with_continuous_replication
    {
        protected static Exception exception;

        private Because of = () =>
                                 {
                                     exception = Catch.Exception(() => server.Replicate<TestDocument>(targetUri));
                                 };

        private It should_replication_without_exceptions = () => exception.ShouldBeNull();
        private It should_call_replicate_via_post = () => commandMock.Verify(x => x.Post(couchUri, replication));
    }
}
using Machine.Specifications;
using Relax.Impl;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_changes_via_longpolling : with_basic_uri
    {
        private Because of = () => uri.Changes(Feed.LongPolling, 10);

        private It should_append_feed_and_since_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax/_changes?feed=longpoll&since=10");
    }
}
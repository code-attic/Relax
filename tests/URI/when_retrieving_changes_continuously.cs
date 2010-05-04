using Machine.Specifications;
using Relax.Impl;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_changes_continuously : with_basic_uri
    {
        private Because of = () => uri.Changes(Feed.Continuous, 10);

        private It should_append_feed_and_since_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/_changes?feed=continuous&since=10");
    }
}
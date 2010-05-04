using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_setting_up_replication : with_basic_uri_without_db
    {
        private Because of = () => { uri.Replicate(); };

        private It should_append_compact_to_uri =
            () => uri.ToString().ShouldEqual(@"http://localhost:5984/_replicate");
    }
}
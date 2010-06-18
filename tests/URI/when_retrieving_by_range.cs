using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_range : with_basic_uri
    {
        private Because of = () => uri.ByRange("id1", "id10");

        private It should_append_start_and_end_keys_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax?startkey=""id1""&endkey=""id10""");
    }
}
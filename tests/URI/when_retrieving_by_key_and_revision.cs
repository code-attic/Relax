using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_key_and_revision : with_basic_uri
    {
        private Because of = () => uri.IdAndRev("id", 2);

        private It should_append_key_and_rev_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/id?rev=2");
    }
}
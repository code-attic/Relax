using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_key : with_basic_uri
    {
        private Because of = () => uri.Id("id");

        private It should_append_id_to_uri 
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/id");
    }
}
using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_inclusive_end_in_view : with_basic_uri
    {
        private Because of = () => uri.InclusiveEnd();

        private It should_append_inclusive_end_true
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax?inclusive_end=true");
    }
}
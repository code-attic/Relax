using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_stale_records_are_ok : with_basic_uri
    {
        private Because of = () => uri.StaleOk();

        private It should_append_stale
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax?stale=ok");
    }
}
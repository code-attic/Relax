using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_bulk_inserting : with_basic_uri
    {
        private Because of = () => uri.BulkInsert();

        private It should_append_bulk_docs
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax/_bulk_docs");
    }
}

using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_cleaning_up_view : with_basic_uri
    {
        private Because of = () => { uri.CleanupViews(); };

        private It should_append_view_cleanup_to_uri =
            () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax/_view_cleanup");
    }
}
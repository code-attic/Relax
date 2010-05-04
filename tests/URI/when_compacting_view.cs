using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_compacting_view : with_basic_uri
    {
        private Because of = () => { uri.CompactView("testView"); };

        private It should_append_compact_to_uri =
            () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/_compact/testView");
    }
}
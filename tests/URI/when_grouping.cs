using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_grouping : with_basic_uri
    {
        private Because of = () => uri.Group();

        private It should_append_group_level_1
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test?group=true&group_level=1");
    }
}
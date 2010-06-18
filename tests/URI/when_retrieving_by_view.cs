using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_view : with_basic_uri
    {
        private Because of = () => uri.View("myView");

        private It should_append_view_name_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/relax/_view/myView");
    }
}
using Machine.Specifications;

namespace Relax.Tests.URI
{
    [Subject("Couch URI")]
    public class when_retrieving_by_named_list_with_descending_and_skip : with_basic_uri
    {
        private Because of = () => uri.List("myList").Descending().Skip(5);

        private It should_append_named_list_and_descending_and_skip_to_uri
            = () => uri.ToString().ShouldEqual(@"http://localhost:5984/test/_list/myList?descending=true&skip=5");
    }
}